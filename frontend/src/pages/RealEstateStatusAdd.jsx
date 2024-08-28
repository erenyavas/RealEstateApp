import React, { useState } from "react";
import { Form, Input, Button, message } from "antd";
import RealEstateStatusService from "../services/realEstateStatus.service";
import { useNavigate } from "react-router-dom";

const RealEstateStatusAdd = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const onFinish = async (values) => {
    setLoading(true);
    try {
      await RealEstateStatusService.addStatus(values);
      message.success("Emlak durumu başarıyla eklendi!");
      navigate("/dashboard/real-estate-status/list");
    } catch (error) {
      message.error("Emlak durumu eklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Form layout="vertical" onFinish={onFinish}>
      <Form.Item label="Emlak Durumu Adı" name="name" rules={[{ required: true, message: "Lütfen emlak durumu adını girin!" }]}>
        <Input />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit" loading={loading}>
          Ekle
        </Button>
      </Form.Item>
    </Form>
  );
};

export default RealEstateStatusAdd;
