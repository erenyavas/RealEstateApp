import React, { useState, useEffect } from "react";
import { Form, Input, Button, message } from "antd";
import RealEstateStatusService from "../services/realEstateStatus.service";
import { useNavigate, useParams } from "react-router-dom";

const RealEstateStatusEdit = () => {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    const fetchStatusDetails = async () => {
      setLoading(true);
      try {
        const response = await RealEstateStatusService.getStatusById(id);
        form.setFieldsValue(response.data);
      } catch (error) {
        message.error("Emlak durumu bilgileri yüklenemedi!");
      } finally {
        setLoading(false);
      }
    };

    fetchStatusDetails();
  }, [id, form]);

  const onFinish = async (values) => {
    setLoading(true);
    try {
      await RealEstateStatusService.updateStatus(id, values);
      message.success("Emlak durumu başarıyla güncellendi!");
      navigate("/dashboard/real-estate-status/list");
    } catch (error) {
      message.error("Emlak durumu güncellenemedi!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Form layout="vertical" form={form} onFinish={onFinish}>
      <Form.Item label="Emlak Durumu Adı" name="name" rules={[{ required: true, message: "Lütfen emlak durumu adını girin!" }]}>
        <Input />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit" loading={loading}>
          Güncelle
        </Button>
      </Form.Item>
    </Form>
  );
};

export default RealEstateStatusEdit;
