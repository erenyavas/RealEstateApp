import apiClient from './api.service';

const RealEstateStatusService = {
    getAllStatuses: () => {
        return apiClient.get('/RealEstateStatus');
    },

    addStatus: (statusData) => {
        return apiClient.post('/RealEstateStatus', statusData);
    },

    deleteStatus: (id) => {
        return apiClient.delete(`/RealEstateStatus/${id}`);
    },

    updateStatus: (id, statusData) => {
        return apiClient.put(`/RealEstateStatus/${id}`, statusData);
    },

    getStatusById: (id) => {
        return apiClient.get(`/RealEstateStatus/${id}`);
    }
};

export default RealEstateStatusService;
