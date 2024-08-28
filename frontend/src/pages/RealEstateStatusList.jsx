import React, { useEffect, useState } from "react";
import { Table, message, Button, Space, Popconfirm } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import RealEstateStatusService from "../services/realEstateStatus.service";

const RealEstateStatusList = () => {
  const [statuses, setStatuses] = useState([]);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchStatuses();
  }, []);

  const fetchStatuses = async () => {
    setLoading(true);
    try {
      const response = await RealEstateStatusService.getAllStatuses();
      setStatuses(response.data);
    } catch (error) {
      message.error("Emlak durumları yüklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await RealEstateStatusService.deleteStatus(id);
      message.success("Emlak durumu başarıyla silindi!");
      fetchStatuses();
    } catch (error) {
      message.error("Emlak durumu silinemedi!");
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
        <Space size="middle">
          <Button type="primary" icon={<EditOutlined />} onClick={() => navigate(`/dashboard/real-estate-status/edit/${record.id}`)}>
            Güncelle
          </Button>
          <Popconfirm title="Bu emlak durumunu silmek istediğinize emin misiniz?" onConfirm={() => handleDelete(record.id)} okText="Evet" cancelText="Hayır">
            <Button type="primary" danger icon={<DeleteOutlined />}>
              Sil
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return <Table columns={columns} dataSource={statuses} loading={loading} rowKey="id" tableLayout="fixed" />;
};

export default RealEstateStatusList;
