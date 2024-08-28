import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { message } from 'antd';

const AdminMiddleware = ({ children }) => {
    const user = useSelector(state => state.user);
    const navigate = useNavigate();

    useEffect(() => {
        if (user.role !== 'Admin') {
            message.error("Bu sayfaya giriş yetkiniz yok.");
            navigate('/');
        }
    }, [user, navigate]);

    if (user.role !== 'Admin') {
        return null;
    }

    return children;
};

export default AdminMiddleware;
