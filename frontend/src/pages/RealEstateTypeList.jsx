import React, { useEffect, useState } from "react";
import { Table, Button, Popconfirm, Breadcrumb, message } from "antd";
import { EditOutlined, DeleteOutlined, UnorderedListOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import realEstateTypeService from "../services/realEstateType.service";

const RealEstateTypeList = () => {
  const [realEstateTypes, setRealEstateTypes] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    loadRealEstateTypes();
  }, []);

  const loadRealEstateTypes = async () => {
    try {
      const response = await realEstateTypeService.getAll();
      if (response && response.data) {
        setRealEstateTypes(response.data);
      } else {
        console.error("Beklenmedik veri formatı:", response);
        message.error("Emlak türleri yüklenirken beklenmedik bir hata oluştu.");
      }
    } catch (error) {
      console.error("Emlak türleri yüklenirken hata oluştu:", error);
      message.error("Emlak türleri yüklenirken bir hata oluştu.");
    }
  };

  const handleDelete = async (id) => {
    try {
      await realEstateTypeService.delete(id);
      loadRealEstateTypes();
      message.success("Emlak türü başarıyla silindi.");
    } catch (error) {
      console.error("Emlak türü silinirken hata oluştu:", error);
      message.error("Emlak türü silinirken bir hata oluştu.");
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
      title: "İşlemler",
      key: "actions",
      render: (text, record) => (
        <>
          <Button type="primary" icon={<EditOutlined />} onClick={() => navigate(`/dashboard/real-estate-types/edit/${record.id}`)}>
            Düzenle
          </Button>
          <Button type="primary" icon={<UnorderedListOutlined />} style={{ marginLeft: 8 }} onClick={() => navigate(`/dashboard/real-estate-types/${record.id}/features`)}>
            Özellikler
          </Button>
          <Popconfirm title="Silmek istediğinize emin misiniz?" onConfirm={() => handleDelete(record.id)} okText="Evet" cancelText="Hayır">
            <Button type="primary" danger icon={<DeleteOutlined />} style={{ marginLeft: 8 }}>
              Sil
            </Button>
          </Popconfirm>
        </>
      ),
    },
  ];

  return (
    <div>
      <Breadcrumb style={{ marginBottom: "16px" }}>
        <Breadcrumb.Item>Dashboard</Breadcrumb.Item>
        <Breadcrumb.Item>Emlak Türleri</Breadcrumb.Item>
      </Breadcrumb>
      <Table dataSource={realEstateTypes} columns={columns} rowKey="id" tableLayout="fixed" />
    </div>
  );
};

export default RealEstateTypeList;
