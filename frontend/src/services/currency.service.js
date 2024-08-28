import apiClient from './api.service';

const CurrencyService = {
    getAllCurrencies: () => {
        return apiClient.get('/currency');
    },

    addCurrency: (currencyData) => {
        return apiClient.post('/currency', currencyData);
    },

    deleteCurrency: (id) => {
        return apiClient.delete(`/currency/${id}`);
    },

    updateCurrency: (id, currencyData) => {
        return apiClient.put(`/currency/${id}`, currencyData);
    },

    getCurrencyById: (id) => {
        return apiClient.get(`/currency/${id}`);
    }
};

export default CurrencyService;
