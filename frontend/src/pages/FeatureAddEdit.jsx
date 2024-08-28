import React, { useState, useEffect } from "react";
import { Form, Input, Button, message, Breadcrumb } from "antd";
import FeatureService from "../services/feature.service";
import { useNavigate, useParams, Link } from "react-router-dom";

const FeatureAddEdit = () => {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const { id, categoryId } = useParams();
  const isEditMode = Boolean(id);

  useEffect(() => {
    if (isEditMode) {
      fetchFeatureDetails();
    }
  }, [id]);

  const fetchFeatureDetails = async () => {
    setLoading(true);
    try {
      const response = await FeatureService.getFeatureById(id);
      form.setFieldsValue(response.data);
    } catch (error) {
      message.error("Özellik bilgileri yüklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const onFinish = async (values) => {
    setLoading(true);
    try {
      values.featureCategoryId = categoryId;
      if (isEditMode) {
        await FeatureService.updateFeature(id, values);
        message.success("Özellik başarıyla güncellendi!");
      } else {
        await FeatureService.addFeature(values);
        message.success("Özellik başarıyla eklendi!");
      }
      navigate(`/dashboard/detail-feature-categories/${categoryId}/features/list`);
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
        <Breadcrumb.Item>{isEditMode ? "Özellik Güncelle" : "Özellik Ekle"}</Breadcrumb.Item>
      </Breadcrumb>
      <Form layout="vertical" form={form} onFinish={onFinish}>
        <Form.Item label="Özellik Adı" name="name" rules={[{ required: true, message: "Lütfen özellik adını girin!" }]}>
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

export default FeatureAddEdit;
