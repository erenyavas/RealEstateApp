import React, { useEffect, useState } from "react";
import { Table, message, Button, Space, Popconfirm, Breadcrumb, Tag } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import DynamicFeatureService from "../services/dynamicFeature.service";

const DynamicFeatureList = () => {
  const [features, setFeatures] = useState([]);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchFeatures();
  }, []);

  const fetchFeatures = async () => {
    setLoading(true);
    try {
      const response = await DynamicFeatureService.getAllFeatures();
      setFeatures(response.data);
    } catch (error) {
      message.error("Emlak türü özellikleri yüklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await DynamicFeatureService.deleteFeature(id);
      message.success("Emlak türü özelliği başarıyla silindi!");
      fetchFeatures();
    } catch (error) {
      message.error("Emlak türü özelliği silinemedi!");
    }
  };

  const renderDataType = (dataType) => {
    switch (dataType) {
      case "number":
        return "Sayı";
      case "string":
        return "Yazı";
      case "options":
        return "Seçenek";
      default:
        return dataType;
    }
  };

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Adı",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "Veri Tipi",
      dataIndex: "dataType",
      key: "dataType",
      render: (dataType) => renderDataType(dataType),
    },
    {
      title: "Min Değer",
      dataIndex: "minValue",
      key: "minValue",
      render: (text) => text || "Yok",
    },
    {
      title: "Max Değer",
      dataIndex: "maxValue",
      key: "maxValue",
      render: (text) => text || "Yok",
    },
    {
      title: "Seçenekler",
      dataIndex: "options",
      key: "options",
      render: (options) =>
        options && options.length > 0
          ? options.map((option) => (
              <Tag bordered={false} color="blue" key={option}>
                {option}
              </Tag>
            ))
          : "Yok",
    },
    {
      title: "İşlemler",
      key: "actions",
      render: (text, record) => (
        <Space size="middle">
          <Button type="primary" icon={<EditOutlined />} onClick={() => navigate(`/dashboard/dynamic-feature/edit/${record.id}`)}>
            Güncelle
          </Button>
          <Popconfirm title="Bu emlak türü özelliğini silmek istediğinize emin misiniz?" onConfirm={() => handleDelete(record.id)} okText="Evet" cancelText="Hayır">
            <Button type="primary" danger icon={<DeleteOutlined />}>
              Sil
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <>
      <Breadcrumb style={{ marginBottom: 16 }}>
        <Breadcrumb.Item>Emlak Türü Özellikleri</Breadcrumb.Item>
        <Breadcrumb.Item>Listele</Breadcrumb.Item>
      </Breadcrumb>
      <Table columns={columns} dataSource={features} loading={loading} rowKey="id" />
    </>
  );
};

export default DynamicFeatureList;
