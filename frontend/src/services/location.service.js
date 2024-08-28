import apiClient from './api.service';

const LocationService = {
    // City
    getAllCities: () => {
        return apiClient.get('/City');
    },

    addCity: (cityData) => {
        return apiClient.post('/City', cityData);
    },

    deleteCity: (id) => {
        return apiClient.delete(`/City/${id}`);
    },

    updateCity: (id, cityData) => {
        return apiClient.put(`/City/${id}`, cityData);
    },

    getCityById: (id) => {
        return apiClient.get(`/City/${id}`);
    },

    // District
    getAllDistricts: () => {
        return apiClient.get('/District');
    },

    addDistrict: (districtData) => {
        return apiClient.post('/District', districtData);
    },

    deleteDistrict: (id) => {
        return apiClient.delete(`/District/${id}`);
    },

    updateDistrict: (id, districtData) => {
        return apiClient.put(`/District/${id}`, districtData);
    },

    getDistrictById: (id) => {
        return apiClient.get(`/District/${id}`);
    },

    getDistrictsByCityId: (cityId) => {
        return apiClient.get(`/District/byCity/${cityId}`);
    },

    // Neighborhood
    getAllNeighborhoods: () => {
        return apiClient.get('/Neighborhood');
    },

    addNeighborhood: (neighborhoodData) => {
        return apiClient.post('/Neighborhood', neighborhoodData);
    },

    deleteNeighborhood: (id) => {
        return apiClient.delete(`/Neighborhood/${id}`);
    },

    updateNeighborhood: (id, neighborhoodData) => {
        return apiClient.put(`/Neighborhood/${id}`, neighborhoodData);
    },

    getNeighborhoodById: (id) => {
        return apiClient.get(`/Neighborhood/${id}`);
    },

    getNeighborhoodsByDistrictId: (districtId) => {
        return apiClient.get(`/Neighborhood/byDistrict/${districtId}`);
    }
};

export default LocationService;
