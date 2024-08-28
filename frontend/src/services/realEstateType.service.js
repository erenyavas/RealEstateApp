import apiClient from './api.service';

const realEstateTypeService = {
    getAll: () => apiClient.get('/RealEstateType'),
    getById: (id) => apiClient.get(`/RealEstateType/${id}`),
    create: (data) => apiClient.post('/RealEstateType', data),
    update: (id, data) => apiClient.put(`/RealEstateType/${id}`, data),
    delete: (id) => apiClient.delete(`/RealEstateType/${id}`),
    addFeatures: (realEstateTypeId, featureIds) =>
        apiClient.post('/RealEstateTypeFeature', { realEstateTypeId, featureIds }),
    deleteFeature: (realEstateTypeId, featureId) =>
        apiClient.delete(`/RealEstateTypeFeature`, {
            params: { realEstateTypeId, featureId },
        }),
    addDetailFeatures: (realEstateTypeId, categoryIds) =>
        apiClient.post('/RealEstateTypeFeatureCategory/AddCategories', { realEstateTypeId, categoryIds }),
    deleteDetailFeature: (realEstateTypeId, featureCategoryId) =>
        apiClient.delete(`/RealEstateTypeFeatureCategory/${realEstateTypeId}/${featureCategoryId}`),
    uploadPhotos: (formData) => {
        return apiClient.post('/RealEstate/uploadPhotos', formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
    },
};

export default realEstateTypeService;
