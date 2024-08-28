import React, { useEffect, useState } from "react";
import { Table, message, Button, Space, Popconfirm } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import CurrencyService from "../services/currency.service";

const CurrencyList = () => {
  const [currencies, setCurrencies] = useState([]);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchCurrencies();
  }, []);

  const fetchCurrencies = async () => {
    setLoading(true);
    try {
      const response = await CurrencyService.getAllCurrencies();
      setCurrencies(response.data);
    } catch (error) {
      message.error("Para birimleri yüklenemedi!");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await CurrencyService.deleteCurrency(id);
      message.success("Para birimi başarıyla silindi!");
      fetchCurrencies();
    } catch (error) {
      message.error("Para birimi silinemedi!");
    }
  };

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Kod",
      dataIndex: "code",
      key: "code",
    },
    {
      title: "Sembol",
      dataIndex: "symbol",
      key: "symbol",
    },
    {
      title: "İşlemler",
      key: "actions",
      render: (text, record) => (
        <Space size="middle">
          <Button type="primary" icon={<EditOutlined />} onClick={() => navigate(`/dashboard/currency/edit/${record.id}`)}>
            Güncelle
          </Button>
          <Popconfirm title="Bu para birimini silmek istediğinize emin misiniz?" onConfirm={() => handleDelete(record.id)} okText="Evet" cancelText="Hayır">
            <Button type="primary" danger icon={<DeleteOutlined />}>
              Sil
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return <Table columns={columns} dataSource={currencies} loading={loading} rowKey="id" tableLayout="fixed" />;
};

export default CurrencyList;
