import React, { useState, useEffect } from "react";
import { Form, Input, Button, InputNumber, Select, message, Breadcrumb } from "antd";
import DynamicFeatureService from "../services/dynamicFeature.service";
import { useNavigate, useParams } from "react-router-dom";

const { Option } = Select;

const DynamicFeatureEdit = () => {
  const [loading, setLoading] = useState(false);
  const [dataType, setDataType] = useState("");
  const [options, setOptions] = useState([]);
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    const fetchFeatureDetails = async () => {
      setLoading(true);
      try {
        const response = await DynamicFeatureService.getFeatureById(id);
        if (response.data.options) {
          setOptions(response.data.options);
        }
        setDataType(response.data.dataType);
        form.setFieldsValue(response.data);
      } catch (error) {
        message.error("Emlak türü özelliği bilgileri yüklenemedi!");
      } finally {
        setLoading(false);
      }
    };

    fetchFeatureDetails();
  }, [id, form]);

  const onFinish = async (values) => {
    setLoading(true);
    try {
      if (dataType === "options") {
        values.options = options;
      }
      await DynamicFeatureService.updateFeature(id, values);
      message.success("Emlak türü özelliği başarıyla güncellendi!");
      navigate("/dashboard/dynamic-feature/list");
    } catch (error) {
      message.error("Emlak türü özelliği güncellenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const handleDataTypeChange = (value) => {
    setDataType(value);
  };

  const handleOptionsChange = (value) => {
    setOptions(value);
  };

  return (
    <>
      <Breadcrumb style={{ marginBottom: 16 }}>
        <Breadcrumb.Item>Emlak Türü Özellikleri</Breadcrumb.Item>
        <Breadcrumb.Item>Güncelle</Breadcrumb.Item>
      </Breadcrumb>
      <Form layout="vertical" form={form} onFinish={onFinish}>
        <Form.Item label="Emlak Türü Özelliği Adı" name="name" rules={[{ required: true, message: "Lütfen emlak türü özelliği adını girin!" }]}>
          <Input />
        </Form.Item>
        <Form.Item label="Veri Tipi" name="dataType" rules={[{ required: true, message: "Lütfen veri tipini seçin!" }]}>
          <Select onChange={handleDataTypeChange} value={dataType}>
            <Option value="number">Sayı</Option>
            <Option value="string">Yazı</Option>
            <Option value="options">Seçenek</Option>
          </Select>
        </Form.Item>
        {(dataType === "number" || dataType === "string") && (
          <>
            <Form.Item label={dataType === "number" ? "Minimum Değer" : "Minimum Uzunluk"} name="minValue">
              <InputNumber style={{ width: "100%" }} />
            </Form.Item>
            <Form.Item label={dataType === "number" ? "Maksimum Değer" : "Maksimum Uzunluk"} name="maxValue">
              <InputNumber style={{ width: "100%" }} />
            </Form.Item>
          </>
        )}
        {dataType === "options" && (
          <Form.Item label="Seçenekler" name="options" rules={[{ required: true, message: "Lütfen en az bir seçenek girin!" }]}>
            <Select mode="tags" style={{ width: "100%" }} placeholder="Seçenek eklemek için enter tuşuna basın" onChange={handleOptionsChange} value={options}>
              {options.map((option) => (
                <Option key={option}>{option}</Option>
              ))}
            </Select>
          </Form.Item>
        )}
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Güncelle
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default DynamicFeatureEdit;
