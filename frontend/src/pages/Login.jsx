import React from "react";
import { Button, Checkbox, Form, Input, Typography, Card, message } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import apiClient from "../services/api.service";
import { setUser } from "../store/userSlice";

const { Title } = Typography;

const LoginPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const onFinish = async (values) => {
    try {
      const loginResponse = await apiClient.post("/Auth/login", values);
      const token = loginResponse.data.token;
      localStorage.setItem("token", token);

      const profileResponse = await apiClient.get("/Profile");

      dispatch(
        setUser({
          firstName: profileResponse.data.firstName,
          lastName: profileResponse.data.lastName,
          role: profileResponse.data.roles[0],
        })
      );

      message.success("Giriş başarılı!");
      if (profileResponse.data.roles[0] === "Admin") {
        navigate("/dashboard");
      } else if (profileResponse.data.roles[0] === "User") {
        navigate("/");
      }
    } catch (error) {
      message.error("E-posta veya şifre hatalı.");
    }
  };

  return (
    <div className="login-container">
      <Card className="login-card">
        <Title level={2} className="login-title">
          Giriş Yap
        </Title>
        <Form name="login_form" className="login-form" initialValues={{ remember: true }} onFinish={onFinish}>
          <Form.Item name="email" rules={[{ required: true, message: "Lütfen e-posta adresinizi girin!", type: "email" }]}>
            <Input prefix={<UserOutlined className="site-form-item-icon" />} placeholder="E-posta" />
          </Form.Item>
          <Form.Item name="password" rules={[{ required: true, message: "Lütfen şifrenizi girin!" }]}>
            <Input prefix={<LockOutlined className="site-form-item-icon" />} type="password" placeholder="Şifre" />
          </Form.Item>
          <Form.Item>
            <Form.Item name="remember" valuePropName="checked" noStyle>
              <Checkbox>Beni hatırla</Checkbox>
            </Form.Item>
            <a className="login-form-forgot" href="">
              Şifrenizi mi unuttunuz?
            </a>
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" className="login-form-button">
              Giriş Yap
            </Button>
            <Link to="/register">
              <a style={{ float: "right" }} href="">
                Şimdi kayıt olun!
              </a>
            </Link>
          </Form.Item>
        </Form>
      </Card>
    </div>
  );
};

export default LoginPage;
