import React from 'react';
import { useTranslation } from 'react-i18next';
import { AppstoreAddOutlined } from '@ant-design/icons';
import { Breadcrumb, Layout, Menu, theme, Button } from 'antd';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import LanguageSwitcherButton from '../components/LanguageSwitcherButton';

const { Header, Content, Footer } = Layout;

const WebsiteLayout = ({ children }) => {
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const user = useSelector(state => state.user);
    const { t } = useTranslation();

    return (
        <Layout>
            <Header
                style={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                }}
            >
                <Link to="/">
                    <div className="demo-logo" />
                </Link>
                <Menu
                    theme="dark"
                    mode="horizontal"
                    defaultSelectedKeys={['2']}
                    selectable={false}
                    style={{
                        minWidth: 600,
                    }}
                >
                    {(user.firstName !== "") ? (
                        <>
                            <Link to="/my/real-estates">
                                <Menu.Item key="my-real-estates" className="nav-item" style={{ marginLeft: 'auto' }}>
                                    {t('myRealEstates')}
                                </Menu.Item>
                            </Link>
                            <Link to="/logout">
                                <Menu.Item key="logout" className="nav-item" style={{ marginLeft: 'auto' }}>
                                    {t('logout')}
                                </Menu.Item>
                            </Link>
                        </>
                    ) : (
                        <>
                            <Link to="/login">
                                <Menu.Item key="login" className="nav-item" style={{ marginLeft: 'auto' }}>
                                    {t('login')}
                                </Menu.Item>
                            </Link>
                            <Link to="/register">
                                <Menu.Item key="register" className="nav-item">
                                    {t('register')}
                                </Menu.Item>
                            </Link>
                        </>
                    )}

                    <Menu.Item key="postAd" className="nav-item">
                        <Link to="/post-ad">
                            <Button type="primary" icon={<AppstoreAddOutlined />} className="post-ad-button">
                                {t('postAd')}
                            </Button>
                        </Link>
                    </Menu.Item>

                    <Menu.Item key="languageSwitcher" className="nav-item">
                        <LanguageSwitcherButton />
                    </Menu.Item>
                </Menu>
            </Header>
            <Content
                style={{
                    padding: '0 48px',
                    width: '1360px',
                    margin: '0 auto',
                }}
            >
                <Breadcrumb
                    style={{
                        margin: '16px ',
                    }}
                >

                    <Breadcrumb.Item>{t('home')}</Breadcrumb.Item>
                    <Breadcrumb.Item>{t('list')}</Breadcrumb.Item>


                </Breadcrumb>
                <Layout
                    style={{
                        padding: '24px 0',
                        background: colorBgContainer,
                        borderRadius: borderRadiusLG,
                    }}
                >
                    <Content
                        style={{
                            padding: '0 24px',
                            minHeight: 280,
                        }}
                    >
                        {children}
                    </Content>
                </Layout>
            </Content>
            <Footer
                style={{
                    textAlign: 'center',
                }}
            >
                {t('footer')} ©{new Date().getFullYear()} {t('createdBy')}
            </Footer>
        </Layout>
    );
};

export default WebsiteLayout;
