import React, { useEffect, useState } from "react";
import RealEstateService from "../services/realEstate.service";
import RealEstateTypeService from "../services/realEstateType.service";
import RealEstateStatusService from "../services/realEstateStatus.service";
import { Row, Col, Card, Statistic, Spin, Typography } from "antd";
import { HomeOutlined, CheckCircleOutlined } from "@ant-design/icons";

const { Title } = Typography;

const Dashboard = () => {
  const [loading, setLoading] = useState(true);
  const [realEstateTypeCounts, setRealEstateTypeCounts] = useState([]);
  const [realEstateStatusCounts, setRealEstateStatusCounts] = useState([]);

  useEffect(() => {
    const fetchRealEstateTypeCounts = async () => {
      try {
        setLoading(true);

        const typeResponse = await RealEstateTypeService.getAll();
        const realEstateTypes = typeResponse.data;

        const countsPromises = realEstateTypes.map(async (type) => {
          const response = await RealEstateService.getAllRealEstates({
            RealEstateTypeIds: [type.id],
            PageNumber: 1,
            PageSize: 1,
          });
          return { type: type.name, count: response.data.totalRecords };
        });

        const counts = await Promise.all(countsPromises);
        setRealEstateTypeCounts(counts);

        setLoading(false);
      } catch (error) {
        console.error("Error fetching real estate type counts:", error);
        setLoading(false);
      }
    };

    const fetchRealEstateStatusCounts = async () => {
      try {
        setLoading(true);

        const statusResponse = await RealEstateStatusService.getAllStatuses();
        const realEstateStatuses = statusResponse.data;

        const countsPromises = realEstateStatuses.map(async (status) => {
          const response = await RealEstateService.getAllRealEstates({
            RealEstateStatusIds: [status.id],
            PageNumber: 1,
            PageSize: 1,
          });
          return { status: status.name, count: response.data.totalRecords };
        });

        const counts = await Promise.all(countsPromises);
        setRealEstateStatusCounts(counts);

        setLoading(false);
      } catch (error) {
        console.error("Error fetching real estate status counts:", error);
        setLoading(false);
      }
    };

    fetchRealEstateTypeCounts();
    fetchRealEstateStatusCounts();
  }, []);

  if (loading) {
    return <Spin tip="Loading..." />;
  }

  return (
    <>
      <Title level={3}>Tiplere Göre Emlaklar</Title>
      <Row gutter={[16, 16]}>
        {realEstateTypeCounts.map((item) => (
          <Col xs={24} sm={12} md={8} lg={6} key={item.type}>
            <Card
              hoverable
              style={{
                background: "#fafafa",
                borderRadius: 8,
                boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
              }}
            >
              <Statistic title={item.type} value={item.count} prefix={<HomeOutlined />} valueStyle={{ fontSize: "24px", fontWeight: "bold" }} />
            </Card>
          </Col>
        ))}
      </Row>

      <Title level={3} style={{ marginTop: "32px" }}>
        Durumlara Göre Emlaklar
      </Title>
      <Row gutter={[16, 16]}>
        {realEstateStatusCounts.map((item) => (
          <Col xs={24} sm={12} md={8} lg={6} key={item.status}>
            <Card
              hoverable
              style={{
                background: "#fafafa",
                borderRadius: 8,
                boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
              }}
            >
              <Statistic title={item.status} value={item.count} prefix={<CheckCircleOutlined />} valueStyle={{ fontSize: "24px", fontWeight: "bold" }} />
            </Card>
          </Col>
        ))}
      </Row>
    </>
  );
};

export default Dashboard;
