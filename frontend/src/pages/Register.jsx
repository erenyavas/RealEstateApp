import React from "react";
import { Button, Form, Input, Typography, Card, message } from "antd";
import { UserOutlined, LockOutlined, MailOutlined, PhoneOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import apiClient from "../services/api.service";

const { Title } = Typography;

const Register = () => {
  const navigate = useNavigate();

  const onFinish = async (values) => {
    try {
      if (values.password !== values.confirmPassword) {
        message.error("Passwords do not match!");
        return;
      }

      const registerResponse = await apiClient.post("/Auth/register", {
        firstName: values.firstName,
        lastName: values.lastName,
        email: values.email,
        phoneNumber: values.phoneNumber,
        password: values.password,
      });

      if (registerResponse.status === 200) {
        message.success("Kayıt başarılı!");
        navigate("/login");
      } else {
        message.error("Kayıt başarısız!");
      }
    } catch (error) {
      message.error(error.response.data.message);
    }
  };

  return (
    <Card style={{ maxWidth: 400, margin: "0 auto", marginTop: "100px" }}>
      <Title level={2} style={{ textAlign: "center" }}>
        Register
      </Title>
      <Form name="register" onFinish={onFinish}>
        <Form.Item name="firstName" rules={[{ required: true, message: "Please input your first name!" }]}>
          <Input prefix={<UserOutlined />} placeholder="First Name" />
        </Form.Item>

        <Form.Item name="lastName" rules={[{ required: true, message: "Please input your last name!" }]}>
          <Input prefix={<UserOutlined />} placeholder="Last Name" />
        </Form.Item>

        <Form.Item
          name="email"
          rules={[
            { required: true, message: "Please input your email!" },
            { type: "email", message: "The input is not a valid email!" },
          ]}
        >
          <Input prefix={<MailOutlined />} placeholder="Email" />
        </Form.Item>

        <Form.Item name="phoneNumber" rules={[{ required: true, message: "Please input your phone number!" }]}>
          <Input prefix={<PhoneOutlined />} placeholder="Phone Number" />
        </Form.Item>

        <Form.Item name="password" rules={[{ required: true, message: "Please input your password!" }]}>
          <Input.Password prefix={<LockOutlined />} placeholder="Password" />
        </Form.Item>

        <Form.Item
          name="confirmPassword"
          dependencies={["password"]}
          hasFeedback
          rules={[
            { required: true, message: "Please confirm your password!" },
            ({ getFieldValue }) => ({
              validator(_, value) {
                if (!value || getFieldValue("password") === value) {
                  return Promise.resolve();
                }
                return Promise.reject(new Error("The two passwords do not match!"));
              },
            }),
          ]}
        >
          <Input.Password prefix={<LockOutlined />} placeholder="Confirm Password" />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" block>
            Register
          </Button>
        </Form.Item>
      </Form>
    </Card>
  );
};

export default Register;
