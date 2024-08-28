import React, { useEffect, useState } from "react";
import { Card, Descriptions, Typography, Image, Divider, List, Spin, Row, Col, Avatar, Breadcrumb } from "antd";
import realEstateService from "../services/realEstate.service";
import realEstateTypeService from "../services/realEstateType.service";
import { useParams } from "react-router-dom";
import { UserOutlined, MailOutlined, PhoneOutlined, EnvironmentOutlined, CheckCircleTwoTone } from "@ant-design/icons";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";
import { useTranslation } from "react-i18next";

delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: require("leaflet/dist/images/marker-icon-2x.png"),
  iconUrl: require("leaflet/dist/images/marker-icon.png"),
  shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
});

const { Title, Text } = Typography;

const RealEstateDetailPage = () => {
  const { t } = useTranslation();
  const [realEstate, setRealEstate] = useState(null);
  const [realEstateTypeData, setRealEstateTypeData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [selectedImage, setSelectedImage] = useState(null);

  const { id } = useParams();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await realEstateService.getRealEstateById(id);
        setRealEstate(response.data);
        if (response.data.photos && response.data.photos.length > 0) {
          setSelectedImage(`${process.env.REACT_APP_API_BASE_URL}/uploads/${response.data.photos[0]}`);
        } else {
          setSelectedImage(`${process.env.REACT_APP_API_BASE_URL}/uploads/no-image.jpg`);
        }
        const typeResponse = await realEstateTypeService.getById(response.data.realEstateTypeId);
        setRealEstateTypeData(typeResponse.data);
      } catch (error) {
        console.error("Error fetching real estate details:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  if (loading) {
    return <Spin size="large" style={{ display: "block", margin: "auto" }} />;
  }

  if (!realEstate) {
    return <div>{t("noDataFound")}</div>;
  }

  const handleThumbnailClick = (photoUrl) => {
    setSelectedImage(photoUrl);
  };

  const isFeaturePresent = (featureId) => {
    return realEstate.realEstateFeatures.some((feature) => feature.featureId === featureId);
  };

  return (
    <div style={{ padding: "0px 20px" }}>
      <Title style={{ marginBottom: "-10px" }} level={4}>
        {realEstate.title}
      </Title>
      <Divider />
      <Row gutter={16}>
        <Col span={10}>
          <Image height={300} src={selectedImage} alt={t("selectedRealEstate")} style={{ marginBottom: "16px", objectFit: "cover" }} />
          <Divider />
          <div style={{ display: "flex", overflowX: "auto" }}>
            {(realEstate.photos && realEstate.photos.length > 0 ? realEstate.photos : ["no-image.jpg"]).map((photo, index) => (
              <div key={index} onClick={() => handleThumbnailClick(`${process.env.REACT_APP_API_BASE_URL}/uploads/${photo}`)} style={{ marginRight: "8px" }}>
                <Image
                  width={100}
                  height={75}
                  src={`${process.env.REACT_APP_API_BASE_URL}/uploads/${photo}`}
                  alt={`${t("thumbnail")} ${index + 1}`}
                  preview={false}
                  style={{
                    cursor: "pointer",
                    border: selectedImage === `${process.env.REACT_APP_API_BASE_URL}/uploads/${photo}` ? "2px solid #1890ff" : "2px solid transparent",
                  }}
                />
              </div>
            ))}
          </div>
        </Col>
        <Col span={8}>
          <Text style={{ fontSize: "20px", fontWeight: "bold" }}>
            {realEstate.price.toLocaleString()} {realEstate.currency.symbol}
          </Text>
          <br />
          <Breadcrumb
            style={{ marginTop: "8px", marginBottom: "-10px" }}
            items={[
              {
                href: `/?cityIds=${realEstate.cityId}`,
                title: (
                  <>
                    <EnvironmentOutlined />
                    <span>{realEstate.city.name}</span>
                  </>
                ),
              },
              {
                href: `/?cityIds=${realEstate.cityId}&districtIds=${realEstate.districtId}`,
                title: (
                  <>
                    <EnvironmentOutlined />
                    <span>{realEstate.district.name}</span>
                  </>
                ),
              },
              {
                href: `/?cityIds=${realEstate.cityId}&districtIds=${realEstate.districtId}&neighborhoodIds=${realEstate.neighborhoodId}`,
                title: (
                  <>
                    <EnvironmentOutlined />
                    <span>{realEstate.neighborhood.name}</span>
                  </>
                ),
              },
            ]}
          />
          <Divider />

          <Descriptions bordered size="small" column={1}>
            <Descriptions.Item label={t("startDate")}>{new Date(realEstate.startDate).toLocaleDateString()}</Descriptions.Item>
            <Descriptions.Item label={t("category")}>{realEstate.realEstateType.name}</Descriptions.Item>
            <Descriptions.Item label={t("status")}>{realEstate.realEstateStatus.name}</Descriptions.Item>
            {(realEstate.realEstateFeatureValues || []).map((item) => (
              <Descriptions.Item key={item.feature.id} label={item.feature.name}>
                {item.value}
              </Descriptions.Item>
            ))}
          </Descriptions>
        </Col>
        <Col span={6}>
          <Card style={{ textAlign: "center" }}>
            <Title level={4}>{t("contact")}</Title>
            <div style={{ display: "flex", alignItems: "center", marginBottom: "8px" }}>
              <Avatar size={40} icon={<UserOutlined />} style={{ marginRight: "8px" }} />
              <div>
                <Text style={{ fontWeight: "bold" }}>{`${realEstate.owner.firstName} ${realEstate.owner.lastName}`}</Text>
              </div>
            </div>
            <div style={{ display: "flex", alignItems: "center", marginBottom: "8px" }}>
              <MailOutlined style={{ fontSize: "20px", marginRight: "8px", color: "#1890ff" }} />
              <Text>{realEstate.owner.email}</Text>
            </div>
            <div style={{ display: "flex", alignItems: "center" }}>
              <PhoneOutlined style={{ fontSize: "20px", marginRight: "8px", color: "#1890ff" }} />
              <Text>{realEstate.owner.phoneNumber || "-"}</Text>
            </div>
          </Card>
        </Col>
      </Row>
      <Divider />
      <Title level={4}>{t("description")}</Title>
      <Text>{realEstate.description}</Text>
      <Divider />
      <Title level={4}>{t("features")}</Title>
      {realEstateTypeData.realEstateTypeFeatureCategories && (
        <List
          bordered
          dataSource={realEstateTypeData.realEstateTypeFeatureCategories}
          renderItem={(category) => (
            <List.Item>
              <List.Item.Meta
                title={category.featureCategory.name}
                description={category.featureCategory.features.map((feature) => (
                  <span
                    key={feature.id}
                    style={{
                      marginRight: "16px",
                      color: isFeaturePresent(feature.id) ? "#000000" : "inherit",
                    }}
                  >
                    {isFeaturePresent(feature.id) && <CheckCircleTwoTone twoToneColor="#52c41a" />} {feature.name}
                  </span>
                ))}
              />
            </List.Item>
          )}
        />
      )}
      <Divider />
      <Title level={4}>{t("location")}</Title>
      <MapContainer center={[realEstate.x, realEstate.y]} zoom={13} style={{ height: "400px", width: "100%" }}>
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors' />
        <Marker position={[realEstate.x, realEstate.y]}>
          <Popup>{realEstate.title}</Popup>
        </Marker>
      </MapContainer>
    </div>
  );
};

export default RealEstateDetailPage;
