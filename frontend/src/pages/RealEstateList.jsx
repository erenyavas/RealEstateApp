import React, { useEffect, useState } from "react";
import { Layout, Table, Image, Row, Col, Typography, Select, InputNumber, Button, Radio, Input, DatePicker, Space, Popconfirm, message } from "antd";
import { useTranslation } from "react-i18next";
import { EditOutlined, DeleteOutlined, EyeOutlined } from "@ant-design/icons";
import realEstateService from "../services/realEstate.service";
import locationService from "../services/location.service";
import realEstateTypeService from "../services/realEstateType.service";
import currencyService from "../services/currency.service";
import realEstateStatusService from "../services/realEstateStatus.service";
import { useSelector } from "react-redux";

const { Title, Text } = Typography;
const { Content, Sider } = Layout;
const { RangePicker } = DatePicker;

const RealEstateList = () => {
  const { t } = useTranslation();
  const [realEstates, setRealEstates] = useState([]);
  const [loading, setLoading] = useState(true);
  const [pagination, setPagination] = useState({ current: 1, pageSize: 4, total: 0 });
  const [cities, setCities] = useState([]);
  const [districts, setDistricts] = useState([]);
  const [neighborhoods, setNeighborhoods] = useState([]);
  const [realEstateTypes, setRealEstateTypes] = useState([]);
  const [currencies, setCurrencies] = useState([]);
  const [realEstateStatuses, setRealEstateStatuses] = useState([]);
  const [filters, setFilters] = useState({
    cityIds: [],
    districtIds: [],
    neighborhoodIds: [],
    realEstateTypeIds: [],
    currencyIds: [],
    statusIds: [],
    minPrice: null,
    maxPrice: null,
    title: "",
    description: "",
    minStartDate: null,
    maxStartDate: null,
    minEndDate: null,
    maxEndDate: null,
  });
  const [sortBy, setSortBy] = useState("startDate");
  const [sortOrder, setSortOrder] = useState("descend");
  const [resetTrigger, setResetTrigger] = useState(false);
  const user = useSelector((state) => state.user);
  const isAdmin = user.role === "Admin";

  const sortOptions = [
    { label: t("priceHighToLow"), value: "price_descend" },
    { label: t("priceLowToHigh"), value: "price_ascend" },
    { label: t("dateNewToOld"), value: "startDate_descend" },
    { label: t("dateOldToNew"), value: "startDate_ascend" },
    { label: t("addressAToZ"), value: "cityId_ascend" },
    { label: t("addressZToA"), value: "cityId_descend" },
  ];

  const handleSortChange = (value) => {
    setPagination({
      ...pagination,
      current: 1,
      pageSize: pagination.pageSize,
    });
    const [sortBy, sortOrder] = value.split("_");
    setSortBy(sortBy);
    setSortOrder(sortOrder);
  };

  useEffect(() => {
    fetchFilterOptions();
  }, []);

  const fetchFilterOptions = async () => {
    try {
      const cityResponse = await locationService.getAllCities();
      setCities(cityResponse.data);
      const typeResponse = await realEstateTypeService.getAll();
      setRealEstateTypes(typeResponse.data);
      const currencyResponse = await currencyService.getAllCurrencies();
      setCurrencies(currencyResponse.data);
      const statusResponse = await realEstateStatusService.getAllStatuses();
      setRealEstateStatuses(statusResponse.data);
    } catch (error) {
      console.error("Error fetching filter options:", error);
    }
  };

  useEffect(() => {
    if (filters.cityIds.length === 1) {
      fetchDistricts(filters.cityIds[0]);
    } else {
      setDistricts([]);
      setNeighborhoods([]);
    }
  }, [filters.cityIds]);

  useEffect(() => {
    if (filters.districtIds.length === 1) {
      fetchNeighborhoods(filters.districtIds[0]);
    } else {
      setNeighborhoods([]);
    }
  }, [filters.districtIds]);

  const fetchDistricts = async (cityId) => {
    try {
      const response = await locationService.getDistrictsByCityId(cityId);
      setDistricts(response.data);
    } catch (error) {
      console.error("Error fetching districts:", error);
    }
  };

  const fetchNeighborhoods = async (districtId) => {
    try {
      const response = await locationService.getNeighborhoodsByDistrictId(districtId);
      setNeighborhoods(response.data);
    } catch (error) {
      console.error("Error fetching neighborhoods:", error);
    }
  };

  useEffect(() => {
    fetchRealEstates();
    resetTrigger && setResetTrigger(false);
  }, [pagination.current, pagination.pageSize, sortBy, sortOrder, resetTrigger]);

  const fetchRealEstates = async () => {
    setLoading(true);
    try {
      const requestParams = {
        PageNumber: pagination.current,
        PageSize: pagination.pageSize,
        CityIds: filters.cityIds,
        DistrictIds: filters.districtIds,
        NeighborhoodIds: filters.neighborhoodIds,
        RealEstateTypeIds: filters.realEstateTypeIds,
        RealEstateStatusIds: filters.statusIds,
        CurrencyIds: filters.currencyIds,
        MinPrice: filters.minPrice,
        MaxPrice: filters.maxPrice,
        Title: filters.title,
        Description: filters.description,
        MinStartDate: filters.minStartDate,
        MaxStartDate: filters.maxStartDate,
        MinEndDate: filters.minEndDate,
        MaxEndDate: filters.maxEndDate,
        SortBy: sortBy,
        SortOrder: sortOrder,
      };

      if (!isAdmin) {
        requestParams.OwnerId = user.userId;
      }

      const response = await realEstateService.getAllRealEstates(requestParams);
      setRealEstates(response.data.items);
      setPagination((prev) => ({ ...prev, total: response.data.totalRecords }));
    } catch (error) {
      console.error("Error fetching real estates:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await realEstateService.deleteRealEstate(id);
      message.success("Emlak kaydı başarıyla silindi!");
      fetchRealEstates();
    } catch (error) {
      console.error("Error deleting real estate:", error);
    }
  };

  const columns = [
    isAdmin && {
      title: "Id",
      dataIndex: "id",
      key: "id",
    },
    {
      title: t("title"),
      dataIndex: "title",
      key: "title",
    },
    {
      title: t("realEstateTypeStatus"),
      dataIndex: "realEstateType",
      key: "realEstateType",
      render: (realEstateType, record) => `${record.realEstateStatus.name} / ${realEstateType.name}`,
    },
    {
      title: t("price"),
      dataIndex: "price",
      key: "price",
      render: (price, record) => `${price.toLocaleString()} ${record.currency.symbol}`,
    },
    {
      title: t("startDate"),
      dataIndex: "startDate",
      key: "startDate",
      render: (date) => new Date(date).toLocaleString(),
    },
    {
      title: t("endDate"),
      dataIndex: "endDate",
      key: "endDate",
      render: (date) => new Date(date).toLocaleString(),
    },
    {
      title: `${t("city")}/${t("district")}/${t("neighborhood")}`,
      key: "location",
      render: (record) => `${record.city.name} / ${record.district.name} / ${record.neighborhood.name}`,
    },
    {
      title: t("owner"),
      dataIndex: "owner",
      key: "owner",
      render: (owner) => `${owner.firstName} ${owner.lastName}`,
    },
    {
      title: t("actions"),
      key: "actions",
      render: (text, record) => (
        <Space size="middle">
          <Button
            icon={<EyeOutlined />}
            onClick={() => {
              window.location.href = `/real-estates/${record.id}`;
            }}
          >
            {t("view")}
          </Button>
          <Button
            icon={<EditOutlined />}
            onClick={() => {
              if (isAdmin) {
                window.location.href = `edit/${record.id}`;
              } else {
                window.location.href = `/my/real-estates/edit/${record.id}`;
              }
            }}
          >
            {t("edit")}
          </Button>
          <Popconfirm title={t("areYouSureDelete")} onConfirm={() => handleDelete(record.id)} okText={t("yes")} cancelText={t("no")}>
            <Button icon={<DeleteOutlined />} danger>
              {t("delete")}
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ].filter(Boolean);

  const handleTableChange = (pagination) => {
    setPagination({
      ...pagination,
      current: pagination.current,
      pageSize: pagination.pageSize,
    });
  };

  const resetFilters = () => {
    setFilters({
      cityIds: [],
      districtIds: [],
      neighborhoodIds: [],
      realEstateTypeIds: [],
      currencyIds: [],
      statusIds: [],
      minPrice: null,
      maxPrice: null,
      title: "",
      description: "",
      minStartDate: null,
      maxStartDate: null,
      minEndDate: null,
      maxEndDate: null,
    });
    setDistricts([]);
    setNeighborhoods([]);
    setPagination({ ...pagination, current: 1, pageSize: pagination.pageSize });
    setResetTrigger(true);
  };

  return (
    <Layout style={{ padding: "20px", background: "#fff" }}>
      <Row gutter={16}>
        <Col span={isAdmin ? 4 : 6}>
          <Sider width={270} style={{ background: "#f9f9f9", padding: "20px", width: "300px", borderRadius: "16px" }}>
            <Title level={4}>{t("filters")}</Title>
            <Text>{t("selectCity")}</Text>
            <Select
              mode="multiple"
              placeholder={t("selectCity")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, cityIds: value, districtIds: [], neighborhoodIds: [] })}
              value={filters.cityIds}
            >
              {cities.map((city) => (
                <Select.Option key={city.id} value={city.id}>
                  {city.name}
                </Select.Option>
              ))}
            </Select>
            <Text>{t("selectDistrict")}</Text>
            <Select
              mode="multiple"
              placeholder={t("selectDistrict")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, districtIds: value, neighborhoodIds: [] })}
              value={filters.districtIds}
              disabled={filters.cityIds.length !== 1}
            >
              {districts.map((district) => (
                <Select.Option key={district.id} value={district.id}>
                  {district.name}
                </Select.Option>
              ))}
            </Select>
            <Text>{t("selectNeighborhood")}</Text>
            <Select
              mode="multiple"
              placeholder={t("selectNeighborhood")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, neighborhoodIds: value })}
              value={filters.neighborhoodIds}
              disabled={filters.districtIds.length !== 1}
            >
              {neighborhoods.map((neighborhood) => (
                <Select.Option key={neighborhood.id} value={neighborhood.id}>
                  {neighborhood.name}
                </Select.Option>
              ))}
            </Select>
            <Text>{t("selectRealEstateType")}</Text>
            <Select
              mode="multiple"
              placeholder={t("selectRealEstateType")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, realEstateTypeIds: value })}
              value={filters.realEstateTypeIds}
            >
              {realEstateTypes.map((type) => (
                <Select.Option key={type.id} value={type.id}>
                  {type.name}
                </Select.Option>
              ))}
            </Select>
            <Text>{t("selectStatus")}</Text>
            <Select
              mode="multiple"
              placeholder={t("selectStatus")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, statusIds: value })}
              value={filters.statusIds}
            >
              {realEstateStatuses.map((status) => (
                <Select.Option key={status.id} value={status.id}>
                  {status.name}
                </Select.Option>
              ))}
            </Select>
            <Text>{t("selectCurrency")}</Text>
            <Select
              mode="multiple"
              placeholder={t("selectCurrency")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, currencyIds: value })}
              value={filters.currencyIds}
            >
              {currencies.map((currency) => (
                <Select.Option key={currency.id} value={currency.id}>
                  {currency.code} ({currency.symbol})
                </Select.Option>
              ))}
            </Select>
            <Text>{t("minPrice")}</Text>
            <InputNumber
              placeholder={t("minPrice")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, minPrice: value })}
              value={filters.minPrice}
              formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ".")}
              parser={(value) => value.replace(/\./g, "")}
            />
            <Text>{t("maxPrice")}</Text>
            <InputNumber
              placeholder={t("maxPrice")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(value) => setFilters({ ...filters, maxPrice: value })}
              value={filters.maxPrice}
              formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ".")}
              parser={(value) => value.replace(/\./g, "")}
            />
            <Text>{t("searchTitle")}</Text>
            <Input placeholder={t("searchTitle")} style={{ width: "100%", marginBottom: "16px" }} onChange={(e) => setFilters({ ...filters, title: e.target.value })} value={filters.title} />
            <Text>{t("searchDescription")}</Text>
            <Input
              placeholder={t("searchDescription")}
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(e) => setFilters({ ...filters, description: e.target.value })}
              value={filters.description}
            />
            <Text>{t("selectStartDateRange")}</Text>
            <RangePicker
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(dates) => {
                setFilters({
                  ...filters,
                  minStartDate: dates ? dates[0].startOf("day").toISOString() : null,
                  maxStartDate: dates ? dates[1].endOf("day").toISOString() : null,
                });
              }}
            />
            <Text>{t("selectEndDateRange")}</Text>
            <RangePicker
              style={{ width: "100%", marginBottom: "16px" }}
              onChange={(dates) => {
                setFilters({
                  ...filters,
                  minEndDate: dates ? dates[0].startOf("day").toISOString() : null,
                  maxEndDate: dates ? dates[1].endOf("day").toISOString() : null,
                });
              }}
            />
            <Button
              type="primary"
              style={{ width: "100%", marginBottom: "8px" }}
              onClick={() => {
                setPagination({
                  ...pagination,
                  current: 1,
                  pageSize: pagination.pageSize,
                });
                fetchRealEstates();
              }}
            >
              {t("applyFilters")}
            </Button>
            <Button type="default" style={{ width: "100%" }} onClick={resetFilters}>
              {t("resetFilters")}
            </Button>
          </Sider>
        </Col>
        <Col span={isAdmin ? 20 : 18}>
          <Content style={{ padding: "20px" }}>
            <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: "16px" }}>
              <Select placeholder={t("sortBy")} style={{ width: "40%" }} onChange={handleSortChange} options={sortOptions} />
            </div>

            <Table
              dataSource={realEstates}
              columns={columns}
              rowKey={(record) => record.id}
              pagination={{
                current: pagination.current,
                pageSize: pagination.pageSize,
                total: pagination.total,
                showTotal: (total) => `${t("total")} ${total} ${t("items")}`,
              }}
              onRow={(record) => {
                return {
                  style: { cursor: "pointer" },
                };
              }}
              onChange={handleTableChange}
              tableLayout="auto"
              scroll={{ x: 1700 }}
              loading={loading}
            />
          </Content>
        </Col>
      </Row>
    </Layout>
  );
};

export default RealEstateList;
