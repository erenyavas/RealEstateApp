import { useEffect } from 'react';
import { message } from 'antd';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setUser } from '../store/userSlice';

const Logout = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        localStorage.removeItem('token');

        dispatch(setUser({ firstName: "", lastName: "", role: "" }));

        message.success("Çıkış başarılı!");

        navigate('/');
    }, [navigate, dispatch]);

    return null;
};

export default Logout;
