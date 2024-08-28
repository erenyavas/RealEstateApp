import React, { useEffect, useState } from "react";
import { Layout, Table, Image, Row, Col, Typography, Select, InputNumber, Button, Radio, Input } from "antd";
import { EnvironmentOutlined } from "@ant-design/icons";
import { useTranslation } from "react-i18next";
import realEstateService from "../services/realEstate.service";
import locationService from "../services/location.service";
import realEstateTypeService from "../services/realEstateType.service";
import currencyService from "../services/currency.service";
import realEstateStatusService from "../services/realEstateStatus.service";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import { useLocation } from "react-router-dom";
import L from "leaflet";
import "leaflet/dist/leaflet.css";

delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: require("leaflet/dist/images/marker-icon-2x.png"),
  iconUrl: require("leaflet/dist/images/marker-icon.png"),
  shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
});

const { Title, Text } = Typography;
const { Content, Sider } = Layout;

const WebsiteRealEstateList = () => {
  const { t } = useTranslation();
  const [realEstates, setRealEstates] = useState([]);
  const [loading, setLoading] = useState(true);
  const [pagination, setPagination] = useState({ current: 1, pageSize: 10, total: 0 });
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
  });
  const [sortBy, setSortBy] = useState("startDate");
  const [sortOrder, setSortOrder] = useState("descend");
  const [viewMode, setViewMode] = useState("list");
  const [resetTrigger, setResetTrigger] = useState(false);
  const [intialLoading, setInitialLoading] = useState(true);

  const location = useLocation();

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
    const searchParams = new URLSearchParams(location.search);
    const cityIds = searchParams.get("cityIds") ? searchParams.get("cityIds").split(",").map(Number) : [];
    const districtIds = searchParams.get("districtIds") ? searchParams.get("districtIds").split(",").map(Number) : [];
    const neighborhoodIds = searchParams.get("neighborhoodIds") ? searchParams.get("neighborhoodIds").split(",").map(Number) : [];

    if (cityIds.length || districtIds.length || neighborhoodIds.length) {
      setFilters((prev) => ({
        ...prev,
        cityIds,
        districtIds,
        neighborhoodIds,
      }));
      setPagination((prev) => ({
        ...prev,
        current: 1,
      }));
    }
    setInitialLoading(false);
    setResetTrigger(true);
  }, [location.search]);

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
    if (!intialLoading && resetTrigger) {
      fetchRealEstates();
      setResetTrigger(false);
    }
  }, [pagination.current, pagination.pageSize, sortBy, sortOrder, resetTrigger]);

  const fetchRealEstates = async () => {
    const currentDate = new Date();
    const futureDate = new Date();
    futureDate.setDate(futureDate.getDate() + 45);

    setLoading(true);
    try {
      const response = await realEstateService.getAllRealEstates({
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
        SortBy: sortBy,
        SortOrder: sortOrder,
        MinEndDate: currentDate.toISOString(),
        MaxEndDate: futureDate.toISOString(),
      });
      setRealEstates(response.data.items);
      setPagination((prev) => ({ ...prev, total: response.data.totalRecords }));
    } catch (error) {
      console.error("Error fetching real estates:", error);
    } finally {
      setLoading(false);
    }
  };

  const columns = [
    {
      title: t("thumbnail"),
      dataIndex: "photos",
      key: "thumbnail",
      render: (photos) => {
        const imageUrl = photos && photos.length > 0 ? `${process.env.REACT_APP_API_BASE_URL}/uploads/${photos[0]}` : `${process.env.REACT_APP_API_BASE_URL}/uploads/no-image.jpg`;
        return <Image preview={false} height={80} src={imageUrl} alt={t("thumbnail")} style={{ objectFit: "cover" }} />;
      },
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
      render: (date) => new Date(date).toLocaleDateString(),
    },
    {
      title: `${t("city")}/${t("district")}`,
      key: "location",
      render: (record) => `${record.city.name} / ${record.district.name}`,
    },
  ];

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
    });
    setDistricts([]);
    setNeighborhoods([]);
    setPagination({ ...pagination, current: 1, pageSize: pagination.pageSize });
    setResetTrigger(true);
  };

  return (
    <Layout style={{ padding: "20px", background: "#fff" }}>
      <Row gutter={16}>
        <Col span={6}>
          <Sider width={270} style={{ background: "#f9f9f9", padding: "20px", width: "300px", borderRadius: "8px" }}>
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
        <Col span={18}>
          <Content style={{ padding: "20px" }}>
            <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: "16px" }}>
              <Radio.Group value={viewMode} onChange={(e) => setViewMode(e.target.value)}>
                <Radio.Button value="list">{t("listView")}</Radio.Button>
                <Radio.Button value="map">{t("mapView")}</Radio.Button>
              </Radio.Group>
              <Select placeholder={t("sortBy")} style={{ width: "40%" }} onChange={handleSortChange} options={sortOptions} />
            </div>

            {viewMode === "list" ? (
              <Table
                dataSource={realEstates}
                columns={columns}
                rowKey={(record) => record.id}
                pagination={{
                  current: pagination.current,
                  pageSize: pagination.pageSize,
                  total: pagination.total,
                  showTotal: (total) => `${t("total")} ${total} ${t("items")}`,
                  showSizeChanger: true,
                }}
                onRow={(record) => {
                  return {
                    onClick: (event) => {
                      window.location.href = `/real-estates/${record.id}`;
                    },
                    style: { cursor: "pointer" },
                  };
                }}
                onChange={handleTableChange}
                tableLayout="fixed"
                loading={loading}
              />
            ) : (
              <MapContainer center={[39.6526021, 27.8875031]} zoom={13} style={{ height: "400px", width: "808px" }}>
                <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors' />
                {realEstates.map((estate) => {
                  const imageUrl =
                    estate.photos && estate.photos.length > 0 ? `${process.env.REACT_APP_API_BASE_URL}/uploads/${estate.photos[0]}` : `${process.env.REACT_APP_API_BASE_URL}/uploads/no-image.jpg`;

                  return (
                    <Marker key={estate.id} position={[estate.x, estate.y]}>
                      <Popup>
                        <div
                          style={{ display: "flex", alignItems: "center", cursor: "pointer" }}
                          onClick={() => {
                            window.location.href = `/real-estates/${estate.id}`;
                          }}
                        >
                          <img src={imageUrl} alt={estate.title} style={{ width: "120px", height: "80px", objectFit: "cover", marginRight: "8px" }} />
                          <div>
                            <p style={{ margin: 0, fontWeight: "bold" }}>{estate.title}</p>
                            <p style={{ margin: 0 }}>
                              <EnvironmentOutlined /> {estate.district.name}, {estate.neighborhood.name}
                            </p>
                            <p style={{ margin: 0, fontWeight: "bold", color: "#910000" }}>
                              {estate.price.toLocaleString()} {estate.currency.symbol}
                            </p>
                          </div>
                        </div>
                      </Popup>
                    </Marker>
                  );
                })}
              </MapContainer>
            )}
          </Content>
        </Col>
      </Row>
    </Layout>
  );
};

export default WebsiteRealEstateList;
