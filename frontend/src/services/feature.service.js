import apiClient from './api.service';

const featureService = {
    getFeaturesByCategoryId: (categoryId) => apiClient.get(`/Feature/GetByCategoryId/${categoryId}`),
    getFeatureById: (id) => apiClient.get(`/Feature/${id}`),
    addFeature: (data) => apiClient.post('/Feature', data),
    updateFeature: (id, data) => apiClient.put(`/Feature/${id}`, data),
    deleteFeature: (id) => apiClient.delete(`/Feature/${id}`)
};

export default featureService;
