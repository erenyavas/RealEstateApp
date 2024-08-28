import apiClient from './api.service';

const DynamicFeatureService = {
    getAllFeatures: () => {
        return apiClient.get('/DynamicFeature');
    },

    addFeature: (featureData) => {
        return apiClient.post('/DynamicFeature', featureData);
    },

    deleteFeature: (id) => {
        return apiClient.delete(`/DynamicFeature/${id}`);
    },

    updateFeature: (id, featureData) => {
        return apiClient.put(`/DynamicFeature/${id}`, featureData);
    },

    getFeatureById: (id) => {
        return apiClient.get(`/DynamicFeature/${id}`);
    }
};

export default DynamicFeatureService;
