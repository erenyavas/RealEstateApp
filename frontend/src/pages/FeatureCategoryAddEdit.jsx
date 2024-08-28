import React, { useState, useEffect } from "react";
import { Form, Input, Button, Select, message, Breadcrumb } from "antd";
import FeatureCategoryService from "../services/featureCategory.service";
import { useNavigate, useParams, Link } from "react-router-dom";

const FeatureCategoryAddEdit = () => {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditMode = Boolean(id);

  useEffect(() => {
    if (isEditMode) {
      fetchCategoryDetails();
    }
  }, [id]);

  const fetchCategoryDetails = async () => {
    setLoading(true);
    try {
      const response = await FeatureCategoryService.getCategoryById(id);
      form.setFieldsValue(response.data);
    } catch (error) {
      message.error("Kategori bilgileri yüklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const onFinish = async (values) => {
    setLoading(true);
    try {
      if (isEditMode) {
        await FeatureCategoryService.updateCategory(id, values);
        message.success("Kategori başarıyla güncellendi!");
      } else {
        await FeatureCategoryService.addCategory(values);
        message.success("Kategori başarıyla eklendi!");
      }
      navigate("/dashboard/detail-feature-categories/list");
    } catch (error) {
      message.error("İşlem başarısız oldu!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Breadcrumb style={{ marginBottom: 16 }}>
        <Breadcrumb.Item>
          <Link to="/dashboard/detail-feature-categories/list">Detay Özellik Kategorileri</Link>
        </Breadcrumb.Item>
        <Breadcrumb.Item>{isEditMode ? "Güncelle" : "Ekle"}</Breadcrumb.Item>
      </Breadcrumb>
      <Form layout="vertical" form={form} onFinish={onFinish}>
        <Form.Item label="Kategori Adı" name="name" rules={[{ required: true, message: "Lütfen kategori adını girin!" }]}>
          <Input />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            {isEditMode ? "Güncelle" : "Ekle"}
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default FeatureCategoryAddEdit;
