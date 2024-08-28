import React, { useState, useEffect, useCallback } from "react";
import { Form, Input, Button, message } from "antd";
import CurrencyService from "../services/currency.service";
import { useNavigate, useParams } from "react-router-dom";

const CurrencyEdit = () => {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const { id } = useParams();

  const fetchCurrencyDetails = useCallback(async () => {
    setLoading(true);
    try {
      const response = await CurrencyService.getCurrencyById(id);
      form.setFieldsValue(response.data);
    } catch (error) {
      message.error("Para birimi bilgileri yüklenemedi!");
    } finally {
      setLoading(false);
    }
  }, [id, form]);

  useEffect(() => {
    fetchCurrencyDetails();
  }, [fetchCurrencyDetails]);

  const onFinish = async (values) => {
    setLoading(true);
    try {
      await CurrencyService.updateCurrency(id, values);
      message.success("Para birimi başarıyla güncellendi!");
      navigate("/dashboard/currency/list");
    } catch (error) {
      message.error("Para birimi güncellenemedi!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Form layout="vertical" form={form} onFinish={onFinish}>
      <Form.Item label="Para Birimi Kodu" name="code" rules={[{ required: true, message: "Lütfen para birimi kodunu girin!" }]}>
        <Input />
      </Form.Item>
      <Form.Item label="Para Birimi Sembolü" name="symbol" rules={[{ required: true, message: "Lütfen para birimi adını girin!" }]}>
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

export default CurrencyEdit;
