import React, { useState } from "react";
import { Form, Input, Button, message } from "antd";
import CurrencyService from "../services/currency.service";
import { useNavigate } from "react-router-dom";

const CurrencyAdd = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const onFinish = async (values) => {
    setLoading(true);
    try {
      await CurrencyService.addCurrency(values);
      message.success("Para birimi başarıyla eklendi!");
      navigate("/dashboard/currency/list");
    } catch (error) {
      message.error("Para birimi eklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Form layout="vertical" onFinish={onFinish}>
      <Form.Item label="Para Birimi Kodu" name="code" rules={[{ required: true, message: "Lütfen para birimi kodunu girin!" }]}>
        <Input />
      </Form.Item>
      <Form.Item label="Para Birimi Sembolü" name="symbol" rules={[{ required: true, message: "Lütfen para birimi adını girin!" }]}>
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

export default CurrencyAdd;
