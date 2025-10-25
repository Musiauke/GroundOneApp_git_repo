export const DRAWER_WIDTH = 260;

export const API_BASE_URL = 'http://localhost:5049/api';

export const MENU_ITEMS = [
  { text: 'Panel główny', icon: 'Dashboard', view: 'dashboard' },
  { text: 'Samochody', icon: 'DirectionsCar', view: 'vehicles' },
  { text: 'Sprzęt', icon: 'Inventory', view: 'items' },
  { text: 'Skrytki', icon: 'Work', view: 'compartments' },
];

export const VEHICLE_TYPES = [
  { value: 'Ambulance', label: 'Ambulance' },
  { value: 'Truck', label: 'Truck' },
  { value: 'Van', label: 'Van' },
  { value: 'Car', label: 'Car' },
];

export const VEHICLE_STATUSES = [
  { value: 'Active', label: 'Active' },
  { value: 'Inactive', label: 'Inactive' },
  { value: 'Maintenance', label: 'Maintenance' },
];