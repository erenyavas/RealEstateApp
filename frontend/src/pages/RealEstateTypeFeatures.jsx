import React, { useEffect, useState } from "react";
import { Table, Button, Breadcrumb, Select, message, Popconfirm, Tag } from "antd";
import { Link, useNavigate, useParams } from "react-router-dom";
import { DeleteOutlined } from "@ant-design/icons";
import realEstateTypeService from "../services/realEstateType.service";
import dynamicFeatureService from "../services/dynamicFeature.service";
import featureCategoryService from "../services/featureCategory.service";

const RealEstateTypeFeatures = () => {
  const [realEstateType, setRealEstateType] = useState(null);
  const [features, setFeatures] = useState([]);
  const [categories, setCategories] = useState([]);
  const [selectedFeatures, setSelectedFeatures] = useState([]);
  const [selectedCategories, setSelectedCategories] = useState([]);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchData = () => {
    realEstateTypeService.getById(id).then((response) => {
      setRealEstateType(response.data);
      setSelectedFeatures(response.data.realEstateTypeFeatures.map((f) => f.featureId));

      setSelectedCategories(response.data.realEstateTypeFeatureCategories.map((c) => c.featureCategoryId));
    });
    dynamicFeatureService.getAllFeatures().then((response) => {
      setFeatures(response.data);
    });
    featureCategoryService.getAllCategories().then((response) => {
      setCategories(response.data);
    });
  };

  useEffect(() => {
    fetchData();
  }, [id]);

  const handleDeleteFeature = (featureId) => {
    realEstateTypeService.deleteFeature(id, featureId).then(() => {
      message.success("Özellik başarıyla silindi.");
      fetchData();
    });
  };

  const handleAddFeature = (featureId) => {
    realEstateTypeService.addFeatures(id, [featureId]).then(() => {
      message.success("Özellik başarıyla eklendi.");
      fetchData();
    });
  };

  const handleDeleteCategory = (categoryId) => {
    realEstateTypeService.deleteDetailFeature(id, categoryId).then(() => {
      message.success("Kategori başarıyla silindi.");
      fetchData();
    });
  };

  const handleAddCategory = (categoryId) => {
    realEstateTypeService.addDetailFeatures(id, [categoryId]).then(() => {
      message.success("Kategori başarıyla eklendi.");
      fetchData();
    });
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

  const featureColumns = [
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
        <Popconfirm title="Bu özelliği silmek istediğinize emin misiniz?" onConfirm={() => handleDeleteFeature(record.id)} okText="Evet" cancelText="Hayır">
          <Button type="primary" danger icon={<DeleteOutlined />}>
            Sil
          </Button>
        </Popconfirm>
      ),
    },
  ];

  const categoryColumns = [
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
      title: "İşlemler",
      key: "actions",
      render: (text, record) => (
        <Popconfirm title="Bu kategoriyi silmek istediğinize emin misiniz?" onConfirm={() => handleDeleteCategory(record.id)} okText="Evet" cancelText="Hayır">
          <Button type="primary" danger icon={<DeleteOutlined />}>
            Sil
          </Button>
        </Popconfirm>
      ),
    },
  ];

  const availableFeatures = features.filter((f) => !selectedFeatures.includes(f.id));
  const availableCategories = categories.filter((c) => !selectedCategories.includes(c.id));

  return (
    <>
      <Breadcrumb>
        <Breadcrumb.Item>
          <Link to="/dashboard">Dashboard</Link>
        </Breadcrumb.Item>
        <Breadcrumb.Item>
          <Link to="/dashboard/real-estate-types/list">Emlak Türleri</Link>
        </Breadcrumb.Item>
        <Breadcrumb.Item>{realEstateType?.name}</Breadcrumb.Item>
        <Breadcrumb.Item>Özellikler ve Kategoriler</Breadcrumb.Item>
      </Breadcrumb>
      <Button type="primary" onClick={() => navigate("/dashboard/real-estate-types/list")} style={{ marginBottom: "16px", float: "right" }}>
        Geri Dön
      </Button>
      <Table columns={featureColumns} dataSource={realEstateType?.realEstateTypeFeatures.map((f) => f.feature)} rowKey="id" style={{ marginBottom: "16px" }} />
      <Select style={{ width: "100%" }} placeholder="Yeni özellik ekleyin" onChange={handleAddFeature}>
        {availableFeatures.map((feature) => (
          <Select.Option key={feature.id} value={feature.id}>
            {feature.name}
          </Select.Option>
        ))}
      </Select>
      <Table columns={categoryColumns} dataSource={realEstateType?.realEstateTypeFeatureCategories.map((c) => c.featureCategory)} rowKey="id" style={{ marginTop: "16px", marginBottom: "16px" }} />
      <Select style={{ width: "100%" }} placeholder="Yeni kategori ekleyin" onChange={handleAddCategory}>
        {availableCategories.map((category) => (
          <Select.Option key={category.id} value={category.id}>
            {category.name}
          </Select.Option>
        ))}
      </Select>
    </>
  );
};

export default RealEstateTypeFeatures;
