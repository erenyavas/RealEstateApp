import apiClient from './api.service';

const featureCategoryService = {
    getAllCategories: () => apiClient.get('/FeatureCategory'),
    getCategoryById: (id) => apiClient.get(`/FeatureCategory/${id}`),
    addCategory: (data) => apiClient.post('/FeatureCategory', data),
    updateCategory: (id, data) => apiClient.put(`/FeatureCategory/${id}`, data),
    deleteCategory: (id) => apiClient.delete(`/FeatureCategory/${id}`)
};

export default featureCategoryService;
