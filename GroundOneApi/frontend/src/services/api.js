import { API_BASE_URL } from '../config/constants';

export const api = {
    // Vehicles
    getVehicles: async () => {
        const response = await fetch(`${API_BASE_URL}/api/vehicle`); 
        if (!response.ok) throw new Error('Failed to fetch vehicles');
        return response.json();
    },

    createVehicle: async (data) => {
        const response = await fetch(`${API_BASE_URL}/api/vehicle`, { 
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to create vehicle');
        return response.json();
    },

    updateVehicle: async (id, data) => {
        const response = await fetch(`${API_BASE_URL}/api/vehicle/${id}`, { 
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to update vehicle');
        return response.json();
    },

    deleteVehicle: async (id) => {
        const response = await fetch(`${API_BASE_URL}/api/vehicle/${id}`, { 
            method: 'DELETE',
        });
        if (!response.ok) throw new Error('Failed to delete vehicle');
    },

    // Items
    getItems: async () => {
        const response = await fetch(`${API_BASE_URL}/api/item`); 
        if (!response.ok) throw new Error('Failed to fetch items');
        return response.json();
    },

    deleteItem: async (id) => {
        const response = await fetch(`${API_BASE_URL}/api/item/${id}`, { 
            method: 'DELETE',
        });
        if (!response.ok) throw new Error('Failed to delete item');
    },

    // Compartments
    getCompartments: async () => {
        const response = await fetch(`${API_BASE_URL}/api/compartment`); 
        if (!response.ok) throw new Error('Failed to fetch compartments');
        return response.json();
    },

    deleteCompartment: async (id) => {
        const response = await fetch(`${API_BASE_URL}/api/compartment/${id}`, { 
            method: 'DELETE',
        });
        if (!response.ok) throw new Error('Failed to delete compartment');
    },
};

export default api;