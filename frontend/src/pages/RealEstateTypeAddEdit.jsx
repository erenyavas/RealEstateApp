import React, { useEffect, useState } from "react";
import { Form, Input, Button, Breadcrumb, Select, message } from "antd";
import { Link, useNavigate, useParams } from "react-router-dom";
import realEstateTypeService from "../services/realEstateType.service";
import dynamicFeatureService from "../services/dynamicFeature.service";
import featureCategoryService from "../services/featureCategory.service";

const { Option } = Select;

const RealEstateTypeAddEdit = () => {
  const [form] = Form.useForm();
  const { id } = useParams();
  const navigate = useNavigate();
  const [features, setFeatures] = useState([]);
  const [categories, setCategories] = useState([]);
  const [selectedFeatures, setSelectedFeatures] = useState([]);
  const [selectedCategories, setSelectedCategories] = useState([]);

  useEffect(() => {
    if (id) {
      realEstateTypeService.getById(id).then((response) => {
        form.setFieldsValue(response.data);
      });
    } else {
      dynamicFeatureService.getAllFeatures().then((response) => {
        setFeatures(response.data);
      });
      featureCategoryService.getAllCategories().then((response) => {
        setCategories(response.data);
      });
    }
  }, [id]);

  const onFinish = (values) => {
    if (id) {
      realEstateTypeService.update(id, values).then(() => {
        message.success("Emlak türü başarıyla güncellendi.");
        navigate("/dashboard/real-estate-types/list");
      });
    } else {
      realEstateTypeService.create({ name: values.name }).then((response) => {
        const typeId = response.data.id;
        realEstateTypeService.addFeatures(typeId, selectedFeatures).then(() => {
          realEstateTypeService.addDetailFeatures(typeId, selectedCategories).then(() => {
            message.success("Emlak türü başarıyla eklendi.");
            navigate("/dashboard/real-estate-types/list");
          });
        });
      });
    }
  };

  const onFeatureChange = (value) => {
    setSelectedFeatures(value);
  };

  const onCategoryChange = (value) => {
    setSelectedCategories(value);
  };

  return (
    <>
      <Breadcrumb>
        <Breadcrumb.Item>
          <Link to="/dashboard">Dashboard</Link>
        </Breadcrumb.Item>
        <Breadcrumb.Item>
          <Link to="/dashboard/real-estate-types/list">Emlak Türleri</Link>
        </Breadcrumb.Item>
        <Breadcrumb.Item>{id ? "Düzenle" : "Ekle"}</Breadcrumb.Item>
      </Breadcrumb>
      <Form form={form} onFinish={onFinish} layout="vertical">
        <Form.Item name="name" label="Tür Adı" rules={[{ required: true, message: "Lütfen tür adı giriniz!" }]}>
          <Input />
        </Form.Item>

        {!id ? (
          <>
            <Form.Item name="featuresId" label="Özellikler">
              <Select mode="multiple" placeholder="Özellik seçin" value={selectedFeatures} onChange={onFeatureChange}>
                {features.map((feature) => (
                  <Option key={feature.id} value={feature.id}>
                    {feature.name}
                  </Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item name="categoriesId" label="Detay Özellik Kategorileri">
              <Select mode="multiple" placeholder="Kategori seçin" value={selectedCategories} onChange={onCategoryChange}>
                {categories.map((category) => (
                  <Option key={category.id} value={category.id}>
                    {category.name}
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </>
        ) : null}

        <Form.Item>
          <Button type="primary" htmlType="submit">
            {id ? "Güncelle" : "Ekle"}
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default RealEstateTypeAddEdit;
