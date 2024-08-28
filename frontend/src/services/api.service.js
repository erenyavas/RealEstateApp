import axios from 'axios';
import { jwtDecode } from 'jwt-decode';


const apiClient = axios.create({
    baseURL: `${process.env.REACT_APP_API_BASE_URL}/api`,
    headers: {
        'Content-Type': 'application/json',
    },
});

apiClient.interceptors.request.use(
    config => {
        const token = localStorage.getItem('token');
        if (token) {
            const decoded = jwtDecode(token);
            const currentTime = Date.now() / 1000;

            if (decoded.exp < currentTime) {
                localStorage.removeItem('token');
                window.location.href = '/login';
                return Promise.reject(new Error('Token expired'));
            }

            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);



export default apiClient;
