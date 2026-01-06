import { API_BASE_URL } from '../config/constants';

export const api = {
    // ============================================
    // VEHICLES - /api/vehicles (PLURAL!)
    // ============================================
    getVehicles: async () => {
        const response = await fetch(`${API_BASE_URL}/api/vehicles`);
        if (!response.ok) throw new Error('Failed to fetch vehicles');
        return response.json();
    },

    createVehicle: async (data) => {
        const response = await fetch(`${API_BASE_URL}/api/vehicles`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to create vehicle');
        return response.json();
    },

    updateVehicle: async (id, data) => {
        const response = await fetch(`${API_BASE_URL}/api/vehicles/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to update vehicle');
        return response.json();
    },

    deleteVehicle: async (id) => {
        const response = await fetch(`${API_BASE_URL}/api/vehicles/${id}`, {
            method: 'DELETE',
        });
        if (!response.ok) throw new Error('Failed to delete vehicle');
    },

    // ============================================
    // ITEMS - /api/items (PLURAL!)
    // ============================================
    getItems: async () => {
        const response = await fetch(`${API_BASE_URL}/api/items`);
        if (!response.ok) throw new Error('Failed to fetch items');
        return response.json();
    },

    createItem: async (data) => {
        const response = await fetch(`${API_BASE_URL}/api/items`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to create item');
        return response.json();
    },

    updateItem: async (id, data) => {
        const response = await fetch(`${API_BASE_URL}/api/items/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to update item');
        return response.json();
    },

    deleteItem: async (id) => {
        const response = await fetch(`${API_BASE_URL}/api/items/${id}`, {
            method: 'DELETE',
        });
        if (!response.ok) throw new Error('Failed to delete item');
    },

    // ============================================
    // COMPARTMENTS - /api/compartments (PLURAL!)
    // ============================================
    getCompartments: async (vehicleId) => {
        const url = vehicleId
            ? `${API_BASE_URL}/api/compartments?vehicleId=${vehicleId}`
            : `${API_BASE_URL}/api/compartments`;
        const response = await fetch(url);
        if (!response.ok) throw new Error('Failed to fetch compartments');
        return response.json();
    },

    createCompartment: async (data) => {
        const response = await fetch(`${API_BASE_URL}/api/compartments`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to create compartment');
        return response.json();
    },

    updateCompartment: async (id, data) => {
        const response = await fetch(`${API_BASE_URL}/api/compartments/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });
        if (!response.ok) throw new Error('Failed to update compartment');
        return response.json();
    },

    deleteCompartment: async (id) => {
        const response = await fetch(`${API_BASE_URL}/api/compartments/${id}`, {
            method: 'DELETE',
        });
        if (!response.ok) throw new Error('Failed to delete compartment');
    },
};