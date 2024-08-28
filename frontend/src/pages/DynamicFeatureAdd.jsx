import React, { useState } from "react";
import { Form, Input, Button, InputNumber, Select, message, Breadcrumb } from "antd";
import DynamicFeatureService from "../services/dynamicFeature.service";
import { useNavigate } from "react-router-dom";

const { Option } = Select;

const DynamicFeatureAdd = () => {
  const [loading, setLoading] = useState(false);
  const [dataType, setDataType] = useState("");
  const [options, setOptions] = useState([]);

  const navigate = useNavigate();

  const onFinish = async (values) => {
    setLoading(true);
    try {
      if (dataType === "options") {
        values.options = options;
      }
      await DynamicFeatureService.addFeature(values);
      message.success("Emlak türü özelliği başarıyla eklendi!");
      navigate("/dashboard/dynamic-feature/list");
    } catch (error) {
      message.error("Emlak türü özelliği eklenemedi!");
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
        <Breadcrumb.Item>Ekle</Breadcrumb.Item>
      </Breadcrumb>
      <Form layout="vertical" onFinish={onFinish}>
        <Form.Item label="Emlak Türü Özelliği Adı" name="name" rules={[{ required: true, message: "Lütfen emlak türü özelliği adını girin!" }]}>
          <Input />
        </Form.Item>
        <Form.Item label="Veri Tipi" name="dataType" rules={[{ required: true, message: "Lütfen veri tipini seçin!" }]}>
          <Select onChange={handleDataTypeChange}>
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
            <Select mode="tags" style={{ width: "100%" }} placeholder="Seçenek eklemek için enter tuşuna basın" onChange={handleOptionsChange}>
              {options.map((option) => (
                <Option key={option}>{option}</Option>
              ))}
            </Select>
          </Form.Item>
        )}
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Ekle
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default DynamicFeatureAdd;
