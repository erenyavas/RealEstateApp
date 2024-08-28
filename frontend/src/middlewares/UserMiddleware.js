import React from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { message } from 'antd';

const UserMiddleware = ({ children }) => {
    const user = useSelector(state => state.user);
    const navigate = useNavigate();

    if (user.role === null || user.role === undefined) {
        message.error("Bu sayfaya giriş yetkiniz yok.")
        navigate('/');
        return null;
    }

    return children;
};

export default UserMiddleware;
