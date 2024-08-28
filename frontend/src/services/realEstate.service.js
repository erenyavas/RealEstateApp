import apiClient from './api.service';

const RealEstateService = {
    getAllRealEstates: (data) => {
        const filteredData = Object.fromEntries(Object.entries(data).filter(([_, v]) => v != null));

        const params = new URLSearchParams();

        Object.keys(filteredData).forEach(key => {
            if (Array.isArray(filteredData[key])) {
                filteredData[key].forEach(value => params.append(key, value));
            } else {
                params.append(key, filteredData[key]);
            }
        });

        console.log(params.toString());

        return apiClient.get(`/RealEstate?${params.toString()}`);
    },


    addRealEstate: (realEstateData) => {
        return apiClient.post('/RealEstate', realEstateData);
    },

    deleteRealEstate: (id) => {
        return apiClient.delete(`/RealEstate/${id}`);
    },

    updateRealEstate: (id, realEstateData) => {
        console.log(realEstateData);
        return apiClient.put(`/RealEstate/${id}`, realEstateData);
    },

    getRealEstateById: (id) => {
        return apiClient.get(`/RealEstate/${id}`);
    },

    uploadPhotos: (photos) => {
        const formData = new FormData();
        photos.forEach(photo => {
            formData.append('files', photo);
        });
        return apiClient.post('/RealEstate/uploadPhotos', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
    },

    addRealEstateFeature: (realEstateFeatureData) => {
        return apiClient.post('/RealEstateFeature/bulk', { ...realEstateFeatureData });
    },

    addRealEstateFeatureValue: (realEstateFeatureValueData) => {
        return apiClient.post('/RealEstateFeatureValue/bulk', { ...realEstateFeatureValueData });
    },



};

export default RealEstateService;
