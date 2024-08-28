import React, { useState, useEffect } from 'react';
import {
    DesktopOutlined,
    FileOutlined,
    PieChartOutlined,
    LogoutOutlined,
    MoneyCollectOutlined,
    HomeOutlined,
    SettingOutlined,
} from '@ant-design/icons';
import { Layout, Menu, Typography, Button } from 'antd';
import { useNavigate, useLocation } from 'react-router-dom';
import LanguageSwitcherButton from '../components/LanguageSwitcherButton';

const { Header, Content, Footer, Sider } = Layout;
const { Title } = Typography;

function getItem(label, key, icon, children) {
    return {
        key,
        icon,
        children,
        label,
    };
}

const items = [
    getItem('Dashboard', '/dashboard', <PieChartOutlined />),
    getItem('Para Birimleri', '/dashboard/currency', <MoneyCollectOutlined />, [
        getItem('Listele', '/dashboard/currency/list'),
        getItem('Ekle', '/dashboard/currency/add'),
    ]),
    getItem('Emlak Yönetimi', '/dashboard/real-estate-management', <HomeOutlined />, [
        getItem('Listele', '/dashboard/real-estates/list'),
        getItem('Ekle', '/dashboard/real-estates/add'),
    ]),
    getItem('Emlak Türleri', '/dashboard/real-estate-types', <SettingOutlined />, [
        getItem('Listele', '/dashboard/real-estate-types/list'),
        getItem('Ekle', '/dashboard/real-estate-types/add'),
    ]),
    getItem('Emlak Durumlar', '/dashboard/real-estate-status', <DesktopOutlined />, [
        getItem('Listele', '/dashboard/real-estate-status/list'),
        getItem('Ekle', '/dashboard/real-estate-status/add'),
    ]),
    getItem('Emlak Türü Özellikleri', '/dashboard/dynamic-feature', <SettingOutlined />, [
        getItem('Listele', '/dashboard/dynamic-feature/list'),
        getItem('Ekle', '/dashboard/dynamic-feature/add'),
    ]),
    getItem('Detay Özellik Kategorileri', '/dashboard/detail-feature-categories', <FileOutlined />, [
        getItem('Listele', '/dashboard/detail-feature-categories/list'),
        getItem('Ekle', '/dashboard/detail-feature-categories/add'),
    ]),
];

const DashboardLayout = ({ children }) => {
    const [collapsed, setCollapsed] = useState(false);
    const navigate = useNavigate();
    const location = useLocation();
    const [openKeys, setOpenKeys] = useState([]);

    useEffect(() => {
        const pathParts = location.pathname.split('/').filter((i) => i);
        if (pathParts.length > 1) {
            setOpenKeys([`/dashboard/${pathParts[1]}`]);
        }
    }, [location]);

    const onMenuClick = (e) => {
        navigate(e.key);
    };

    return (
        <Layout style={{ minHeight: '100vh' }}>
            <Sider
                width={250}
                collapsible
                collapsed={collapsed}
                onCollapse={(value) => setCollapsed(value)}
            >
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '64px' }}>
                    <Title level={2} style={{ color: 'white', margin: 0 }}>
                        Admin
                    </Title>
                </div>
                <Menu
                    theme="dark"
                    mode="inline"
                    selectedKeys={[location.pathname]}
                    openKeys={openKeys}
                    onOpenChange={(keys) => setOpenKeys(keys)}
                    items={items}
                    onClick={onMenuClick}
                />
            </Sider>
            <Layout>
                <Header style={{ display: 'flex', justifyContent: 'flex-end', alignItems: 'center', padding: '0 16px', background: 'white' }}>
                    <LanguageSwitcherButton />
                    <Button style={{ marginLeft: "10px" }} type="primary" icon={<LogoutOutlined />} onClick={() => navigate('/logout')}>
                        Çıkış Yap
                    </Button>
                </Header>
                <Content style={{ margin: '16px' }}>
                    <div
                        style={{
                            padding: 24,
                            minHeight: 360,
                            background: '#fff',
                            borderRadius: '8px',
                        }}
                    >
                        {children}
                    </div>
                </Content>
                <Footer style={{ textAlign: 'center' }}>
                    Admin Panel ©{new Date().getFullYear()} Created by Eren
                </Footer>
            </Layout>
        </Layout>
    );
};

export default DashboardLayout;
