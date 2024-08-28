import React, { useEffect } from 'react';
import apiClient from '../services/api.service';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { jwtDecode } from 'jwt-decode';
import { setUser } from '../store/userSlice';

const AuthMiddleware = ({ children }) => {
    const [mount, setMount] = React.useState(false);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            const decoded = jwtDecode(token);
            const currentTime = Date.now() / 1000;

            if (decoded.exp < currentTime) {
                localStorage.removeItem('token');
                navigate('/login');
            } else {
                apiClient.get('/Profile')
                    .then(response => {
                        dispatch(setUser({
                            firstName: response.data.firstName,
                            lastName: response.data.lastName,
                            role: response.data.roles[0],
                            userId: response.data.userId,
                        }));
                        setMount(true);
                    })
                    .catch(() => {
                        localStorage.removeItem('token');
                        navigate('/login');
                    });
            }
        } else {
            navigate('/login');
        }
    }, [navigate, dispatch]);

    if (!mount) {
        return <div>Loading...</div>;
    }

    return children;
};

export default AuthMiddleware;
