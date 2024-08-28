import React, { useEffect, useState } from "react";
import { Table, Space, Button, Popconfirm, message, Breadcrumb, Row, Col } from "antd";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";
import { useNavigate, useParams, Link } from "react-router-dom";
import featureService from "../services/feature.service";
import featureCategoryService from "../services/featureCategory.service";

const FeatureList = () => {
  const [features, setFeatures] = useState([]);
  const [loading, setLoading] = useState(false);
  const [categoryName, setCategoryName] = useState("");
  const navigate = useNavigate();
  const { categoryId } = useParams();

  useEffect(() => {
    fetchFeatures();
    fetchCategoryName();
  }, [categoryId]);

  const fetchFeatures = async () => {
    setLoading(true);
    try {
      const response = await featureService.getFeaturesByCategoryId(categoryId);
      setFeatures(response.data);
    } catch (error) {
      message.error("Özellikler yüklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const fetchCategoryName = async () => {
    try {
      const response = await featureCategoryService.getCategoryById(categoryId);
      setCategoryName(response.data.name);
    } catch (error) {
      message.error("Kategori adı yüklenemedi!");
    }
  };

  const handleDelete = async (id) => {
    try {
      await featureService.deleteFeature(id);
      message.success("Özellik başarıyla silindi!");
      fetchFeatures();
    } catch (error) {
      message.error("Özellik silinemedi!");
    }
  };

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Özellik Adı",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "İşlemler",
      key: "actions",
      render: (text, record) => (
        <Space size="middle">
          <Button type="primary" icon={<EditOutlined />} onClick={() => navigate(`/dashboard/detail-feature-categories/${categoryId}/features/edit/${record.id}`)}>
            Güncelle
          </Button>
          <Popconfirm title="Bu özelliği silmek istediğinize emin misiniz?" onConfirm={() => handleDelete(record.id)} okText="Evet" cancelText="Hayır">
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
      <Row justify="space-between" align="middle" style={{ marginBottom: 11, marginTop: -5 }}>
        <Col>
          <Breadcrumb>
            <Breadcrumb.Item>
              <Link to="/dashboard/detail-feature-categories/list">Detay Özellik Kategorileri</Link>
            </Breadcrumb.Item>
            <Breadcrumb.Item>{categoryName}</Breadcrumb.Item>
            <Breadcrumb.Item>Özellikler</Breadcrumb.Item>
          </Breadcrumb>
        </Col>
        <Col>
          <Button type="primary" icon={<PlusOutlined />} onClick={() => navigate(`/dashboard/detail-feature-categories/${categoryId}/features/add`)}>
            Özellik Ekle
          </Button>
        </Col>
      </Row>

      <Table columns={columns} dataSource={features} loading={loading} rowKey="id" tableLayout="fixed" />
    </>
  );
};

export default FeatureList;
