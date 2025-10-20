import axios from 'axios';

const API_BASE_URL = 'http://localhost:5173/api'; // Zmień port na właściwy

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor dla obsługi błędów
api.interceptors.response.use(
  response => response,
  error => {
    console.error('API Error:', error);
    return Promise.reject(error);
  }
);

// Vehicles API
export const vehiclesApi = {
  getAll: () => api.get('/vehicle'),
  getById: (id) => api.get(`/vehicle/${id}`),
  create: (data) => api.post('/vehicle', data),
  update: (id, data) => api.put(`/vehicle/${id}`, data),
  delete: (id) => api.delete(`/vehicle/${id}`),
};

// Items API
export const itemsApi = {
  getAll: () => api.get('/item'),
  getById: (id) => api.get(`/item/${id}`),
  create: (data) => api.post('/item', data),
  update: (id, data) => api.put(`/item/${id}`, data),
  delete: (id) => api.delete(`/item/${id}`),
};

// Compartments API
export const compartmentsApi = {
  getAll: () => api.get('/compartment'),
  getById: (id) => api.get(`/compartment/${id}`),
  getByVehicle: (vehicleId) => api.get(`/compartment/vehicle/${vehicleId}`),
  create: (data) => api.post('/compartment', data),
  update: (id, data) => api.put(`/compartment/${id}`, data),
  delete: (id) => api.delete(`/compartment/${id}`),
};

export default api;