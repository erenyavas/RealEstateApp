import React, { useEffect, useState } from "react";
import { Table, Space, Button, Popconfirm, message, Breadcrumb } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import FeatureCategoryService from "../services/featureCategory.service";

const FeatureCategoryList = () => {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    setLoading(true);
    try {
      const response = await FeatureCategoryService.getAllCategories();
      setCategories(response.data);
    } catch (error) {
      message.error("Kategoriler yüklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await FeatureCategoryService.deleteCategory(id);
      message.success("Kategori başarıyla silindi!");
      fetchCategories();
    } catch (error) {
      message.error("Kategori silinemedi!");
    }
  };

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Kategori Adı",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "İşlemler",
      key: "actions",
      render: (text, record) => (
        <Space size="middle">
          <Button type="primary" icon={<EditOutlined />} onClick={() => navigate(`/dashboard/detail-feature-categories/edit/${record.id}`)}>
            Güncelle
          </Button>
          <Popconfirm title="Bu kategoriyi silmek istediğinize emin misiniz?" onConfirm={() => handleDelete(record.id)} okText="Evet" cancelText="Hayır">
            <Button type="primary" danger icon={<DeleteOutlined />}>
              Sil
            </Button>
          </Popconfirm>
          <Button type="primary" onClick={() => navigate(`/dashboard/detail-feature-categories/${record.id}/features/list`)}>
            Özellikler
          </Button>
        </Space>
      ),
    },
  ];

  return (
    <>
      <Breadcrumb style={{ marginBottom: 16 }}>
        <Breadcrumb.Item>Detay Özellik Kategorileri</Breadcrumb.Item>
        <Breadcrumb.Item>Listele</Breadcrumb.Item>
      </Breadcrumb>
      <Table columns={columns} dataSource={categories} loading={loading} rowKey="id" tableLayout="fixed" />
    </>
  );
};

export default FeatureCategoryList;
