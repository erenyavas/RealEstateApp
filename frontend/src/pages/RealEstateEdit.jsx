import React, { useEffect, useState } from "react";
import { Form, Input, Button, Breadcrumb, Select, message, Steps, Card, InputNumber, Descriptions } from "antd";
import { Link, useNavigate, useParams } from "react-router-dom";
import realEstateService from "../services/realEstate.service";
import currencyService from "../services/currency.service";
import realEstateStatusService from "../services/realEstateStatus.service";
import { useSelector } from "react-redux";

const { Option } = Select;
const { Step } = Steps;

const RealEstateEdit = () => {
  const [currentStep, setCurrentStep] = useState(0);
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const { id } = useParams();
  const [currencies, setCurrencies] = useState([]);
  const [realEstateStatuses, setRealEstateStatuses] = useState([]);
  const user = useSelector((state) => state.user);
  const isAdmin = user.role === "Admin";

  useEffect(() => {
    currencyService.getAllCurrencies().then((response) => setCurrencies(response.data));
    realEstateStatusService.getAllStatuses().then((response) => setRealEstateStatuses(response.data));

    realEstateService.getRealEstateById(id).then((response) => {
      form.setFieldsValue(response.data);
    });
  }, [id, form]);

  const onFinish = () => {
    const title = form.getFieldValue("title");
    const description = form.getFieldValue("description");
    const price = form.getFieldValue("price");
    const currencyId = form.getFieldValue("currencyId");
    const realEstateStatusId = form.getFieldValue("realEstateStatusId");

    const updatedRealEstate = {
      title,
      description,
      price,
      currencyId,
      realEstateStatusId,
    };

    realEstateService
      .updateRealEstate(id, updatedRealEstate)
      .then(() => {
        message.success("Emlak başarıyla güncellendi.");
        if (isAdmin) navigate("/dashboard/real-estates/list");
        else navigate("/my/real-estates");
      })
      .catch((error) => {
        message.error("Emlak güncellenirken bir hata oluştu. Lütfen tekrar deneyin.", 3);
        console.error("Emlak güncellenirken bir hata oluştu:", error);
      });
  };

  const next = () => {
    form
      .validateFields()
      .then(() => {
        setCurrentStep(currentStep + 1);
      })
      .catch((error) => {
        message.error("Lütfen gerekli alanları doldurun ve tekrar deneyin.", 3);
        console.error("Validation failed:", error);
      });
  };

  const prev = () => {
    setCurrentStep(currentStep - 1);
  };

  const steps = [
    {
      title: "Düzenle",
      content: (
        <Form form={form} layout="vertical" onFinish={onFinish}>
          <Form.Item name="title" label="Başlık" rules={[{ required: true, message: "Lütfen başlık giriniz!" }]}>
            <Input />
          </Form.Item>
          <Form.Item name="description" label="Açıklama" rules={[{ required: true, message: "Lütfen açıklama giriniz!" }]}>
            <Input.TextArea rows={4} />
          </Form.Item>
          <Form.Item name="price" label="Fiyat" rules={[{ required: true, message: "Lütfen fiyat giriniz!" }]}>
            <InputNumber style={{ width: "100%" }} formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ".")} parser={(value) => value.replace(/\./g, "")} />
          </Form.Item>
          <Form.Item name="currencyId" label="Para Birimi" rules={[{ required: true, message: "Lütfen para birimi seçiniz!" }]}>
            <Select>
              {currencies.map((currency) => (
                <Option key={currency.id} value={currency.id}>
                  {currency.code}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item name="realEstateStatusId" label="Gayrimenkul Durumu" rules={[{ required: true, message: "Lütfen gayrimenkul durumu seçiniz!" }]}>
            <Select>
              {realEstateStatuses.map((status) => (
                <Option key={status.id} value={status.id}>
                  {status.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
        </Form>
      ),
    },
    {
      title: "Önizleme",
      content: (
        <Card style={{ marginTop: "10px" }}>
          <Descriptions bordered title="Emlak Bilgileri" layout="vertical" column={1}>
            <Descriptions.Item label="İlan Başlığı">{form.getFieldValue("title") || "Başlık girilmemiş"}</Descriptions.Item>
            <Descriptions.Item label="Açıklama">{form.getFieldValue("description") || "Açıklama girilmemiş"}</Descriptions.Item>
            <Descriptions.Item label="Fiyat">
              {form.getFieldValue("price") ? form.getFieldValue("price").toLocaleString() : "Fiyat girilmemiş"} {currencies.find((currency) => currency.id === form.getFieldValue("currencyId"))?.code}
            </Descriptions.Item>
            <Descriptions.Item label="Gayrimenkul Durumu">{realEstateStatuses.find((status) => status.id === form.getFieldValue("realEstateStatusId"))?.name || "Durum girilmemiş"}</Descriptions.Item>
          </Descriptions>
        </Card>
      ),
    },
  ];

  return (
    <>
      {isAdmin && (
        <Breadcrumb>
          <Breadcrumb.Item>
            <Link to="/dashboard">Dashboard</Link>
          </Breadcrumb.Item>
          <Breadcrumb.Item>
            <Link to="/dashboard/real-estate/list">Emlaklar</Link>
          </Breadcrumb.Item>
          <Breadcrumb.Item>Düzenle</Breadcrumb.Item>
        </Breadcrumb>
      )}

      <Steps current={currentStep}>
        {steps.map((item) => (
          <Step key={item.title} title={item.title} />
        ))}
      </Steps>
      <div className="steps-content">{steps[currentStep].content}</div>
      <div className="steps-action" style={{ marginTop: "16px" }}>
        {currentStep > 0 && (
          <Button style={{ margin: "0 8px" }} onClick={prev}>
            Önceki
          </Button>
        )}
        {currentStep < steps.length - 1 && (
          <Button type="primary" onClick={next}>
            Sonraki
          </Button>
        )}
        {currentStep === steps.length - 1 && (
          <Button type="primary" onClick={form.submit}>
            Güncelle
          </Button>
        )}
      </div>
    </>
  );
};

export default RealEstateEdit;
