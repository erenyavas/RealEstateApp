import React, { useEffect, useState, useRef } from "react";
import { Form, Input, Button, Breadcrumb, Select, Upload, message, Row, Col, Steps, Card, Divider, Checkbox, Descriptions, Typography, Image, List, InputNumber } from "antd";
import { Link, useNavigate } from "react-router-dom";
import { UploadOutlined, EnvironmentOutlined, CheckCircleTwoTone } from "@ant-design/icons";
import { MapContainer, TileLayer, Marker, useMapEvents } from "react-leaflet";
import L from "leaflet";
import icon from "leaflet/dist/images/marker-icon.png";
import iconShadow from "leaflet/dist/images/marker-shadow.png";
import realEstateService from "../services/realEstate.service";
import currencyService from "../services/currency.service";
import realEstateTypeService from "../services/realEstateType.service";
import realEstateStatusService from "../services/realEstateStatus.service";
import locationService from "../services/location.service";
import { useSelector } from "react-redux";
import "leaflet/dist/leaflet.css";
import { useTranslation } from "react-i18next";

const { Option } = Select;
const { Step } = Steps;
const { Title, Text } = Typography;

let DefaultIcon = L.icon({
  iconUrl: icon,
  shadowUrl: iconShadow,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
});

L.Marker.prototype.options.icon = DefaultIcon;

const RealEstateAdd = () => {
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const [currentStep, setCurrentStep] = useState(0);
  const [formValues, setFormValues] = useState({});
  const [currencies, setCurrencies] = useState([]);
  const [realEstateTypes, setRealEstateTypes] = useState([]);
  const [realEstateStatuses, setRealEstateStatuses] = useState([]);
  const [cities, setCities] = useState([]);
  const [districts, setDistricts] = useState([]);
  const [neighborhoods, setNeighborhoods] = useState([]);
  const [features, setFeatures] = useState([]);
  const [realEstateTypeFeatureCategories, setRealEstateTypeFeatureCategories] = useState([]);
  const [position, setPosition] = useState(null);
  const mapRef = useRef();
  const user = useSelector((state) => state.user);
  const isAdmin = user.role === "Admin";
  const { t } = useTranslation();

  useEffect(() => {
    currencyService.getAllCurrencies().then((response) => setCurrencies(response.data));
    realEstateTypeService.getAll().then((response) => setRealEstateTypes(response.data));
    realEstateStatusService.getAllStatuses().then((response) => setRealEstateStatuses(response.data));
    locationService.getAllCities().then((response) => setCities(response.data));

    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((pos) => {
        const { latitude, longitude } = pos.coords;
        setPosition({ lat: latitude, lng: longitude });
      });
    }
  }, []);

  useEffect(() => {
    if (position && mapRef.current) {
      mapRef.current.setView(position, mapRef.current.getZoom());
    }
  }, [position]);

  const onCityChange = (cityId) => {
    locationService.getDistrictsByCityId(cityId).then((response) => setDistricts(response.data));
    setNeighborhoods([]);
    form.setFieldsValue({ districtId: undefined, neighborhoodId: undefined });
  };

  const onDistrictChange = (districtId) => {
    locationService.getNeighborhoodsByDistrictId(districtId).then((response) => setNeighborhoods(response.data));
    form.setFieldsValue({ neighborhoodId: undefined });
  };

  const onRealEstateTypeChange = (typeId) => {
    realEstateTypeService.getById(typeId).then((response) => {
      setFeatures(response.data.realEstateTypeFeatures);
      setRealEstateTypeFeatureCategories(response.data.realEstateTypeFeatureCategories);
      form.setFieldsValue({ features: response.data.realEstateTypeFeatures });
      form.setFieldsValue({ realEstateTypeFeatureCategories: response.data.realEstateTypeFeatureCategories });
    });
  };

  const LocationMarker = () => {
    useMapEvents({
      click(e) {
        setPosition(e.latlng);
      },
    });

    return position === null ? null : <Marker position={position} />;
  };

  const next = () => {
    form
      .validateFields()
      .then((values) => {
        setFormValues({ ...formValues, ...values });
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

  const onFinish = () => {
    const realEstateData = {
      ...formValues,
      x: position.lat,
      y: position.lng,
      squareMeters: 0, // kullanılmıyor
    };

    if (realEstateData.photos) {
      realEstateData.photos = realEstateData.photos.map((photo) => photo.name);
    }

    //RealEstateFeatureValues
    const realEstateFeatureValuesFeatureIds = []; // kat sayısı, oda sayısı gibi feature'ların id'lerini tutacak array (RealEstateFeatureValues (featureId))
    const values = []; // feature'ların değerlerini tutacak array (RealEstateFeatureValues (value))

    for (const key in realEstateData) {
      if (!key.startsWith("feature_")) continue;
      const id = key.split("_")[1];
      realEstateFeatureValuesFeatureIds.push(Number(id));
      if (realEstateData[key] !== undefined) values.push(realEstateData[key]);
    }

    //RealEstateFeatures
    const featureIds = []; // feature'ların id'lerini tutacak array (RealEstateFeature (featureId))

    for (const key in realEstateData) {
      if (!key.startsWith("category_")) continue;
      if (realEstateData[key] !== undefined) featureIds.push(...realEstateData[key]);
    }

    realEstateService
      .addRealEstate(realEstateData)
      .then((response) => {
        const RealEstateFeatureValuesData = {
          realEstateId: response.data.id,
          featureIds: realEstateFeatureValuesFeatureIds,
          values: values,
        };
        const RealEstateFeatureData = {
          realEstateId: response.data.id,
          featureIds: featureIds,
        };
        realEstateService
          .addRealEstateFeatureValue(RealEstateFeatureValuesData)
          .then(() => {
            realEstateService
              .addRealEstateFeature(RealEstateFeatureData)
              .then(() => {
                message.success("Emlak başarıyla eklendi.");
                isAdmin ? navigate("/dashboard/real-estates/list") : navigate("/my/real-estates");
              })
              .catch((error) => {
                message.error("Emlak eklenirken bir hata oluştu. Lütfen tekrar deneyin.", 3);
                console.error("Emlak eklenirken bir hata oluştu: addRealEstateFeature", error);
              });
          })
          .catch((error) => {
            message.error("Emlak eklenirken bir hata oluştu. Lütfen tekrar deneyin.", 3);
            console.error("Emlak eklenirken bir hata oluştu: addRealEstateFeatureValue", error);
          });
      })
      .catch((error) => {
        message.error("Emlak eklenirken bir hata oluştu. Lütfen tekrar deneyin.", 3);
        console.error("Emlak eklenirken bir hata oluştu:", error);
      });
  };

  const steps = [
    {
      title: "İlan Türü",
      content: (
        <Form form={form} layout="vertical">
          <Form.Item name="realEstateTypeId" label="Gayrimenkul Türü" rules={[{ required: true, message: "Lütfen gayrimenkul türü seçiniz!" }]}>
            <Select onChange={onRealEstateTypeChange}>
              {realEstateTypes.map((type) => (
                <Option key={type.id} value={type.id}>
                  {type.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
        </Form>
      ),
    },
    {
      title: "Detay Bilgiler",
      content: (
        <Form form={form} layout="vertical">
          <Form.Item name="title" label="Başlık" rules={[{ required: true, message: "Lütfen başlık giriniz!" }]}>
            <Input />
          </Form.Item>
          <Form.Item name="description" label="Açıklama" rules={[{ required: true, message: "Lütfen açıklama giriniz!" }]}>
            <Input.TextArea rows={4} />
          </Form.Item>
          <Row gutter={16}>
            <Col xs={24} md={12}>
              <Form.Item name="price" label="Fiyat" rules={[{ required: true, message: "Lütfen fiyat giriniz!" }]}>
                <InputNumber style={{ width: "100%" }} formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ".")} parser={(value) => value.replace(/\./g, "")} />
              </Form.Item>
            </Col>
            <Col xs={24} md={12}>
              <Form.Item name="currencyId" label="Para Birimi" rules={[{ required: true, message: "Lütfen para birimi seçiniz!" }]}>
                <Select>
                  {currencies.map((currency) => (
                    <Option key={currency.id} value={currency.id}>
                      {currency.code}
                    </Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
          </Row>

          <Form.Item name="realEstateStatusId" label="Gayrimenkul Durumu" rules={[{ required: true, message: "Lütfen gayrimenkul durumu seçiniz!" }]}>
            <Select>
              {realEstateStatuses.map((status) => (
                <Option key={status.id} value={status.id}>
                  {status.name}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Divider />

          {features.map((feature) => {
            if (feature.feature.dataType === "number") {
              return (
                <Form.Item
                  key={feature.feature.id}
                  name={`feature_${feature.feature.id}`}
                  label={feature.feature.name}
                  rules={[
                    { required: true, message: `Lütfen ${feature.feature.name} giriniz!` },
                    {
                      validator: (_, value) => {
                        const minValue = feature.feature.minValue;
                        const maxValue = feature.feature.maxValue;

                        if (!value || ((minValue === null || value >= minValue) && (maxValue === null || value <= maxValue))) {
                          return Promise.resolve();
                        } else {
                          return Promise.reject(new Error(`Değer ${minValue !== null ? minValue : "minimum"} ile ${maxValue !== null ? maxValue : "maksimum"} arasında olmalıdır.`));
                        }
                      },
                    },
                  ]}
                >
                  <Input type="number" />
                </Form.Item>
              );
            } else if (feature.feature.dataType === "options") {
              return (
                <Form.Item
                  key={feature.feature.id}
                  name={`feature_${feature.feature.id}`}
                  label={feature.feature.name}
                  rules={[{ required: true, message: `Lütfen ${feature.feature.name} seçiniz!` }]}
                >
                  <Select>
                    {feature.feature.options.map((option) => (
                      <Option key={option} value={option}>
                        {option}
                      </Option>
                    ))}
                  </Select>
                </Form.Item>
              );
            } else if (feature.feature.dataType === "string") {
              return (
                <Form.Item
                  key={feature.feature.id}
                  name={`feature_${feature.feature.id}`}
                  label={feature.feature.name}
                  rules={[
                    { required: true, message: `Lütfen ${feature.feature.name} giriniz!` },
                    { min: feature.feature.minValue, max: feature.feature.maxValue, message: `Uzunluk ${feature.feature.minValue} ile ${feature.feature.maxValue} karakter arasında olmalıdır.` },
                  ]}
                >
                  <Input maxLength={feature.feature.maxValue} />
                </Form.Item>
              );
            } else {
              return null;
            }
          })}

          <Divider />

          {realEstateTypeFeatureCategories.map((category) => (
            <div key={category.featureCategoryId}>
              <h3>{category.featureCategory.name}</h3>
              <Form.Item name={`category_${category.featureCategoryId}`} valuePropName="checkedvalues">
                <Checkbox.Group>
                  {category.featureCategory.features.map((feature) => (
                    <Checkbox key={feature.id} value={feature.id}>
                      {feature.name}
                    </Checkbox>
                  ))}
                </Checkbox.Group>
              </Form.Item>
            </div>
          ))}
          <Divider />
          <Form.Item name="photos" label="Fotoğraflar" valuePropName="fileList" getValueFromEvent={(e) => (Array.isArray(e) ? e : e && e.fileList)}>
            <Upload.Dragger
              name="files"
              action={`${process.env.REACT_APP_API_BASE_URL}/api/RealEstate/uploadPhotos`}
              listType="picture"
              multiple
              headers={{
                Authorization: `Bearer ${localStorage.getItem("token")}`,
              }}
              beforeUpload={(file) => {
                const isJpgOrPng = file.type === "image/jpeg" || file.type === "image/png";
                if (!isJpgOrPng) {
                  message.error("Sadece JPG/PNG dosyaları yükleyebilirsiniz!");
                  return Upload.LIST_IGNORE;
                }
                const isLt5M = file.size / 1024 / 1024 < 5;
                if (!isLt5M) {
                  message.error("Resim 5MB boyutundan büyük olamaz!");
                  return Upload.LIST_IGNORE;
                }
                const uniqueFileName = `${Date.now()}_${Math.floor(Math.random() * 10000000000)}.${file.type.split("/")[1]}`;
                const uniqueFile = new File([file], uniqueFileName, { type: file.type });
                return uniqueFile;
              }}
              onChange={(info) => {
                const { status } = info.file;
                if (status === "done") {
                } else if (status === "error") {
                  message.error(`${info.file.name} yüklenirken hata oluştu.`);
                }
              }}
              accept=".jpg,.jpeg,.png"
              showUploadList={{ showRemoveIcon: true }}
            >
              <p className="ant-upload-drag-icon">
                <UploadOutlined />
              </p>
              <p className="ant-upload-text">Dosyaları buraya sürükleyip bırakın veya yüklemek için tıklayın</p>
              <p className="ant-upload-hint">Sadece JPG/PNG dosyaları kabul edilir ve boyut 5MB'yi geçmemelidir.</p>
            </Upload.Dragger>
          </Form.Item>

          <Divider />

          <Row gutter={16}>
            <Col xs={24} md={8}>
              <Form.Item name="cityId" label="Şehir" rules={[{ required: true, message: "Lütfen şehir seçiniz!" }]}>
                <Select onChange={onCityChange}>
                  {cities.map((city) => (
                    <Option key={city.id} value={city.id}>
                      {city.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
            <Col xs={24} md={8}>
              <Form.Item name="districtId" label="İlçe" rules={[{ required: true, message: "Lütfen ilçe seçiniz!" }]}>
                <Select onChange={onDistrictChange}>
                  {districts.map((district) => (
                    <Option key={district.id} value={district.id}>
                      {district.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
            <Col xs={24} md={8}>
              <Form.Item name="neighborhoodId" label="Mahalle" rules={[{ required: true, message: "Lütfen mahalle seçiniz!" }]}>
                <Select>
                  {neighborhoods.map((neighborhood) => (
                    <Option key={neighborhood.id} value={neighborhood.id}>
                      {neighborhood.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
          </Row>

          <Form.Item label="Konum">
            <MapContainer
              center={position || [41.0082, 28.9784]}
              zoom={13}
              style={{ height: "400px", width: "100%" }}
              whenCreated={(mapInstance) => {
                mapRef.current = mapInstance;
              }}
              ref={mapRef}
            >
              <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
              <LocationMarker />
            </MapContainer>
          </Form.Item>
        </Form>
      ),
    },
    {
      title: "Önizleme",
      content: (
        <Card>
          <Title style={{ marginBottom: "-10px" }} level={4}>
            {form.getFieldValue("title") || "Başlık yok"}
          </Title>
          <Divider />
          <Row gutter={16}>
            <Col span={10}>
              <Image
                height={300}
                src={
                  form.getFieldValue("photos") ? `${process.env.REACT_APP_API_BASE_URL}/uploads/${form.getFieldValue("photos")[0].name}` : `${process.env.REACT_APP_API_BASE_URL}/uploads/no-image.jpg`
                }
                alt="Seçilen Resim"
                style={{ marginBottom: "16px", objectFit: "cover" }}
              />
              <Divider />
              <div style={{ display: "flex", overflowX: "auto" }}>
                {(form.getFieldValue("photos") || ["no-image.jpg"]).map((photo, index) => {
                  const photoSrc =
                    typeof photo === "string" ? `${process.env.REACT_APP_API_BASE_URL}/uploads/${photo}` : `${process.env.REACT_APP_API_BASE_URL}/uploads/${photo.name || "no-image.jpg"}`;

                  return (
                    <div key={index} style={{ marginRight: "8px" }}>
                      <Image
                        width={100}
                        height={75}
                        src={photoSrc}
                        alt={`Thumbnail ${index + 1}`}
                        preview={false}
                        style={{
                          cursor: "pointer",
                          border: "2px solid transparent",
                        }}
                      />
                    </div>
                  );
                })}
              </div>
            </Col>
            <Col span={8}>
              <Text style={{ fontSize: "20px", fontWeight: "bold" }}>
                {form.getFieldValue("price") ? form.getFieldValue("price").toLocaleString() : ""} {currencies.find((currency) => currency.id === form.getFieldValue("currencyId"))?.symbol}
              </Text>
              <br />
              <Breadcrumb
                style={{ marginTop: "8px", marginBottom: "-10px" }}
                items={[
                  {
                    title: (
                      <>
                        <EnvironmentOutlined />
                        <span>{cities.find((city) => city.id === form.getFieldValue("cityId"))?.name}</span>
                      </>
                    ),
                  },
                  {
                    title: (
                      <>
                        <EnvironmentOutlined />
                        <span>{districts.find((district) => district.id === form.getFieldValue("districtId"))?.name}</span>
                      </>
                    ),
                  },
                  {
                    title: (
                      <>
                        <EnvironmentOutlined />
                        <span>{neighborhoods.find((neighborhood) => neighborhood.id === form.getFieldValue("neighborhoodId"))?.name}</span>
                      </>
                    ),
                  },
                ]}
              />
              <Divider />
              <Descriptions bordered size="small" column={1}>
                {/* <Descriptions.Item label="Başlık">{form.getFieldValue("title") || "Başlık yok"}</Descriptions.Item>
                <Descriptions.Item label="Açıklama">{form.getFieldValue("description") || "Açıklama yok"}</Descriptions.Item> */}
                <Descriptions.Item label="Gayrimenkul Türü">{realEstateTypes.find((type) => type.id === form.getFieldValue("realEstateTypeId"))?.name || "Tür belirtilmemiş"}</Descriptions.Item>
                <Descriptions.Item label="Gayrimenkul Durumu">
                  {realEstateStatuses.find((status) => status.id === form.getFieldValue("realEstateStatusId"))?.name || "Durum belirtilmemiş"}
                </Descriptions.Item>
                {features.map((feature) => (
                  <Descriptions.Item key={feature.feature.id} label={feature.feature.name}>
                    {form.getFieldValue(`feature_${feature.feature.id}`)}
                  </Descriptions.Item>
                ))}
              </Descriptions>
            </Col>
            <Col span={6}>
              <Card style={{ textAlign: "center" }}>
                <Title level={4}>Konum</Title>
                <MapContainer center={position || [41.0082, 28.9784]} zoom={13} style={{ height: "200px", width: "100%" }}>
                  <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                  {position && <Marker position={position} />}
                </MapContainer>
              </Card>
            </Col>
          </Row>
          <Divider />
          <Title level={4}>{t("description")}</Title>
          <Text>{form.getFieldValue("description") || "Açıklama girilmemiş"}</Text>
          <Title level={4}>Özellikler</Title>
          {realEstateTypeFeatureCategories && (
            <List
              bordered
              dataSource={realEstateTypeFeatureCategories}
              renderItem={(category) => (
                <List.Item>
                  <List.Item.Meta
                    title={category.featureCategory.name}
                    description={category.featureCategory.features.map((feature) => (
                      <span
                        key={feature.id}
                        style={{
                          marginRight: "16px",
                          color: form.getFieldValue(`category_${category.featureCategoryId}`)?.includes(feature.id) ? "#000000" : "inherit",
                        }}
                      >
                        {form.getFieldValue(`category_${category.featureCategoryId}`)?.includes(feature.id) && <CheckCircleTwoTone twoToneColor="#52c41a" />} {feature.name}
                      </span>
                    ))}
                  />
                </List.Item>
              )}
            />
          )}
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
          <Breadcrumb.Item>Ekle</Breadcrumb.Item>
        </Breadcrumb>
      )}

      <Steps current={currentStep} style={{ marginBottom: "10px" }}>
        {steps.map((item) => (
          <Step key={item.title} title={item.title} />
        ))}
      </Steps>
      <div className="steps-content">{steps[currentStep].content}</div>
      <div className="steps-action" style={{ marginTop: "16px" }}>
        {currentStep > 0 && (
          <Button style={{ margin: "0 8px" }} onClick={() => prev()}>
            Önceki
          </Button>
        )}
        {currentStep < steps.length - 1 && (
          <Button type="primary" onClick={() => next()}>
            Sonraki
          </Button>
        )}
        {currentStep === steps.length - 1 && (
          <Button type="primary" onClick={() => onFinish()}>
            Kaydet
          </Button>
        )}
      </div>
    </>
  );
};

export default RealEstateAdd;
