import React, { useState, useEffect } from 'react';
import {
  Box,
  Drawer,
  AppBar,
  Toolbar,
  List,
  Typography,
  Divider,
  IconButton,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  CssBaseline,
  ThemeProvider,
  createTheme,
  Paper,
  Grid,
  Card,
  CardContent,
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Chip,
  Alert,
  Snackbar,
  CircularProgress,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  MenuItem,
  Badge,
  Avatar,
  Fade,
  Zoom,
  Tooltip,
  ButtonGroup,
  CardActionArea,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import DirectionsCarIcon from '@mui/icons-material/DirectionsCar';
import InventoryIcon from '@mui/icons-material/Inventory';
import WorkIcon from '@mui/icons-material/Work';
import DashboardIcon from '@mui/icons-material/Dashboard';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import VisibilityIcon from '@mui/icons-material/Visibility';
import RefreshIcon from '@mui/icons-material/Refresh';
import LocalFireDepartmentIcon from '@mui/icons-material/LocalFireDepartment';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import BuildIcon from '@mui/icons-material/Build';
import PauseCircleIcon from '@mui/icons-material/PauseCircle';
import TrendingUpIcon from '@mui/icons-material/TrendingUp';
import ViewInArIcon from '@mui/icons-material/ViewInAr';

const drawerWidth = 260;

const theme = createTheme({
  palette: {
    primary: {
      main: '#d21919',
      light: '#ff5252',
      dark: '#9a0007',
    },
    secondary: {
      main: '#13186e',
      light: '#4a4e9e',
      dark: '#000042',
    },
    background: {
      default: '#f5f7fa',
      paper: '#ffffff',
    },
  },
  typography: {
    fontFamily: '"Inter", "Roboto", "Helvetica", "Arial", sans-serif',
    h4: {
      fontWeight: 700,
    },
    h6: {
      fontWeight: 600,
    },
  },
  shape: {
    borderRadius: 12,
  },
  components: {
    MuiCard: {
      styleOverrides: {
        root: {
          boxShadow: '0 2px 12px rgba(0,0,0,0.08)',
          transition: 'transform 0.2s, box-shadow 0.2s',
          '&:hover': {
            transform: 'translateY(-4px)',
            boxShadow: '0 8px 24px rgba(0,0,0,0.12)',
          },
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          fontWeight: 600,
        },
      },
    },
  },
});

const API_BASE_URL = 'http://localhost:5049/api';

const api = {
  getVehicles: async () => {
    const response = await fetch(`${API_BASE_URL}/vehicle`);
    if (!response.ok) throw new Error('Failed to fetch vehicles');
    return response.json();
  },
  createVehicle: async (data) => {
    const response = await fetch(`${API_BASE_URL}/vehicle`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    });
    if (!response.ok) throw new Error('Failed to create vehicle');
    return response.json();
  },
  updateVehicle: async (id, data) => {
    const response = await fetch(`${API_BASE_URL}/vehicle/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    });
    if (!response.ok) throw new Error('Failed to update vehicle');
    return response.json();
  },
  deleteVehicle: async (id) => {
    const response = await fetch(`${API_BASE_URL}/vehicle/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) throw new Error('Failed to delete vehicle');
  },
  getItems: async () => {
    const response = await fetch(`${API_BASE_URL}/Items`);
    if (!response.ok) throw new Error('Failed to fetch items');
    return response.json();
  },
  deleteItem: async (id) => {
    const response = await fetch(`${API_BASE_URL}/Items/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) throw new Error('Failed to delete item');
  },
  getCompartments: async () => {
    const response = await fetch(`${API_BASE_URL}/compartment`);
    if (!response.ok) throw new Error('Failed to fetch compartments');
    return response.json();
  },
  deleteCompartment: async (id) => {
    const response = await fetch(`${API_BASE_URL}/compartment/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) throw new Error('Failed to delete compartment');
  },
};

// Komponent wizualizacji pojazdu
function VehicleVisualization({ vehicle, compartments, onCompartmentClick }) {
  const [currentView, setCurrentView] = useState('left');

  const getCompartmentsByView = (view) => {
    return compartments.filter(c => {
      if (view === 'left') return c.location?.toLowerCase().includes('left') || c.location?.toLowerCase().includes('lew');
      if (view === 'right') return c.location?.toLowerCase().includes('right') || c.location?.toLowerCase().includes('praw');
      if (view === 'rear') return c.location?.toLowerCase().includes('rear') || c.location?.toLowerCase().includes('tył');
      return false;
    });
  };

  const viewCompartments = getCompartmentsByView(currentView);

  return (
    <Paper elevation={0} sx={{ p: 4, borderRadius: 3, background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)' }}>
      <Box sx={{ mb: 3, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Typography variant="h5" sx={{ color: 'white', fontWeight: 700 }}>
          {vehicle?.make} {vehicle?.model} - {vehicle?.registrationNumber}
        </Typography>
        <ButtonGroup variant="contained" sx={{ bgcolor: 'rgba(255,255,255,0.2)' }}>
          <Button
            onClick={() => setCurrentView('left')}
            sx={{
              bgcolor: currentView === 'left' ? 'white' : 'transparent',
              color: currentView === 'left' ? 'primary.main' : 'white',
              '&:hover': { bgcolor: currentView === 'left' ? 'white' : 'rgba(255,255,255,0.1)' }
            }}
          >
            Lewa strona
          </Button>
          <Button
            onClick={() => setCurrentView('right')}
            sx={{
              bgcolor: currentView === 'right' ? 'white' : 'transparent',
              color: currentView === 'right' ? 'primary.main' : 'white',
              '&:hover': { bgcolor: currentView === 'right' ? 'white' : 'rgba(255,255,255,0.1)' }
            }}
          >
            Prawa strona
          </Button>
          <Button
            onClick={() => setCurrentView('rear')}
            sx={{
              bgcolor: currentView === 'rear' ? 'white' : 'transparent',
              color: currentView === 'rear' ? 'primary.main' : 'white',
              '&:hover': { bgcolor: currentView === 'rear' ? 'white' : 'rgba(255,255,255,0.1)' }
            }}
          >
            Tył
          </Button>
        </ButtonGroup>
      </Box>

      <Box sx={{ position: 'relative', height: 300, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
        {/* Sylwetka pojazdu */}
        <Box
          sx={{
            width: 600,
            height: 200,
            background: 'linear-gradient(180deg, #d21919 0%, #9a0007 100%)',
            borderRadius: 2,
            position: 'relative',
            boxShadow: '0 20px 60px rgba(0,0,0,0.3)',
          }}
        >
          {/* Kabina */}
          <Box
            sx={{
              position: 'absolute',
              left: 0,
              top: 0,
              width: 80,
              height: '100%',
              background: 'linear-gradient(180deg, #ff5252 0%, #d21919 100%)',
              borderRadius: '8px 0 0 8px',
            }}
          >
            <Box sx={{ position: 'absolute', left: 8, top: 16, width: 64, height: 32, bgcolor: 'rgba(135,206,250,0.6)', borderRadius: 1 }} />
          </Box>

          {/* Kompartmenty */}
          {viewCompartments.map((comp, index) => (
            <Zoom key={comp.id} in={true} style={{ transitionDelay: `${index * 100}ms` }}>
              <Tooltip title={`${comp.name} - ${comp.capacity || 0} przedmiotów`} arrow>
                <Card
                  onClick={() => onCompartmentClick(comp)}
                  sx={{
                    position: 'absolute',
                    left: 120 + index * 140,
                    top: 40,
                    width: 120,
                    height: 120,
                    bgcolor: 'rgba(0,0,0,0.7)',
                    cursor: 'pointer',
                    transition: 'all 0.3s',
                    '&:hover': {
                      bgcolor: 'rgba(255,255,255,0.9)',
                      transform: 'scale(1.05)',
                      '& .compartment-icon': {
                        color: 'primary.main',
                      },
                      '& .compartment-text': {
                        color: 'text.primary',
                      }
                    }
                  }}
                >
                  <CardContent sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', height: '100%' }}>
                    <WorkIcon className="compartment-icon" sx={{ fontSize: 40, color: 'white', mb: 1, transition: 'color 0.3s' }} />
                    <Typography className="compartment-text" variant="caption" sx={{ color: 'white', fontWeight: 600, textAlign: 'center', transition: 'color 0.3s' }}>
                      Skrytka {index + 1}
                    </Typography>
                    <Typography className="compartment-text" variant="caption" sx={{ color: 'rgba(255,255,255,0.7)', fontSize: 10, transition: 'color 0.3s' }}>
                      {comp.capacity || 0} przedmiotów
                    </Typography>
                  </CardContent>
                </Card>
              </Tooltip>
            </Zoom>
          ))}

          {/* Koła */}
          <Box sx={{ position: 'absolute', bottom: -16, left: 32, width: 32, height: 32, bgcolor: '#1a1a1a', borderRadius: '50%', border: '4px solid #333' }} />
          <Box sx={{ position: 'absolute', bottom: -16, left: 96, width: 32, height: 32, bgcolor: '#1a1a1a', borderRadius: '50%', border: '4px solid #333' }} />
          <Box sx={{ position: 'absolute', bottom: -16, right: 96, width: 32, height: 32, bgcolor: '#1a1a1a', borderRadius: '50%', border: '4px solid #333' }} />
          <Box sx={{ position: 'absolute', bottom: -16, right: 32, width: 32, height: 32, bgcolor: '#1a1a1a', borderRadius: '50%', border: '4px solid #333' }} />
        </Box>
      </Box>

      <Typography variant="body2" sx={{ color: 'rgba(255,255,255,0.8)', textAlign: 'center', mt: 3 }}>
        Kliknij na skrytkę, aby zobaczyć jej zawartość
      </Typography>
    </Paper>
  );
}

// Dialog formularza edycji pojazdu
function VehicleEditDialog({ open, vehicle, onClose, onSave }) {
  const [formData, setFormData] = useState({
    registrationNumber: '',
    make: '',
    model: '',
    vehicleType: '',
    status: '',
  });

  useEffect(() => {
    if (vehicle) {
      setFormData({
        registrationNumber: vehicle.registrationNumber || '',
        make: vehicle.make || '',
        model: vehicle.model || '',
        vehicleType: vehicle.vehicleType || '',
        status: vehicle.status || '',
      });
    }
  }, [vehicle]);

  const handleChange = (field) => (event) => {
    setFormData({ ...formData, [field]: event.target.value });
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth PaperProps={{ sx: { borderRadius: 3 } }}>
      <DialogTitle sx={{ pb: 1 }}>
        <Typography variant="h6" fontWeight={700}>Edytuj pojazd</Typography>
      </DialogTitle>
      <DialogContent>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}>
          <TextField
            label="Numer rejestracyjny"
            value={formData.registrationNumber}
            onChange={handleChange('registrationNumber')}
            fullWidth
            required
          />
          <TextField
            label="Marka"
            value={formData.make}
            onChange={handleChange('make')}
            fullWidth
            required
          />
          <TextField
            label="Model"
            value={formData.model}
            onChange={handleChange('model')}
            fullWidth
            required
          />
          <TextField
            select
            label="Typ pojazdu"
            value={formData.vehicleType}
            onChange={handleChange('vehicleType')}
            fullWidth
            required
          >
            <MenuItem value="Ambulance">Ambulance</MenuItem>
            <MenuItem value="Truck">Truck</MenuItem>
            <MenuItem value="Van">Van</MenuItem>
            <MenuItem value="Car">Car</MenuItem>
          </TextField>
          <TextField
            select
            label="Status"
            value={formData.status}
            onChange={handleChange('status')}
            fullWidth
            required
          >
            <MenuItem value="Active">Active</MenuItem>
            <MenuItem value="Inactive">Inactive</MenuItem>
            <MenuItem value="Maintenance">Maintenance</MenuItem>
          </TextField>
        </Box>
      </DialogContent>
      <DialogActions sx={{ px: 3, pb: 3 }}>
        <Button onClick={onClose} color="inherit">Anuluj</Button>
        <Button onClick={() => onSave(formData)} variant="contained" color="primary">
          Zapisz
        </Button>
      </DialogActions>
    </Dialog>
  );
}

// Dialog formularza dodawania pojazdu
function VehicleAddDialog({ open, onClose, onSave }) {
  const [formData, setFormData] = useState({
    registrationNumber: '',
    make: '',
    model: '',
    vehicleType: 'Ambulance',
    status: 'Active',
  });

  const handleChange = (field) => (event) => {
    setFormData({ ...formData, [field]: event.target.value });
  };

  const handleSubmit = () => {
    onSave(formData);
    setFormData({
      registrationNumber: '',
      make: '',
      model: '',
      vehicleType: 'Ambulance',
      status: 'Active',
    });
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth PaperProps={{ sx: { borderRadius: 3 } }}>
      <DialogTitle sx={{ pb: 1 }}>
        <Typography variant="h6" fontWeight={700}>Dodaj nowy pojazd</Typography>
      </DialogTitle>
      <DialogContent>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}>
          <TextField
            label="Numer rejestracyjny"
            value={formData.registrationNumber}
            onChange={handleChange('registrationNumber')}
            fullWidth
            required
          />
          <TextField
            label="Marka"
            value={formData.make}
            onChange={handleChange('make')}
            fullWidth
            required
          />
          <TextField
            label="Model"
            value={formData.model}
            onChange={handleChange('model')}
            fullWidth
            required
          />
          <TextField
            select
            label="Typ pojazdu"
            value={formData.vehicleType}
            onChange={handleChange('vehicleType')}
            fullWidth
            required
          >
            <MenuItem value="Ambulance">Ambulance</MenuItem>
            <MenuItem value="Truck">Truck</MenuItem>
            <MenuItem value="Van">Van</MenuItem>
            <MenuItem value="Car">Car</MenuItem>
          </TextField>
          <TextField
            select
            label="Status"
            value={formData.status}
            onChange={handleChange('status')}
            fullWidth
            required
          >
            <MenuItem value="Active">Active</MenuItem>
            <MenuItem value="Inactive">Inactive</MenuItem>
            <MenuItem value="Maintenance">Maintenance</MenuItem>
          </TextField>
        </Box>
      </DialogContent>
      <DialogActions sx={{ px: 3, pb: 3 }}>
        <Button onClick={onClose} color="inherit">Anuluj</Button>
        <Button onClick={handleSubmit} variant="contained" color="primary">
          Dodaj
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default function AdminPanel() {
  const [mobileOpen, setMobileOpen] = useState(false);
  const [currentView, setCurrentView] = useState('dashboard');
  const [vehicles, setVehicles] = useState([]);
  const [items, setItems] = useState([]);
  const [compartments, setCompartments] = useState([]);
  const [loading, setLoading] = useState(false);
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });
  const [vehicleAddOpen, setVehicleAddOpen] = useState(false);
  const [vehicleEditOpen, setVehicleEditOpen] = useState(false);
  const [editingVehicle, setEditingVehicle] = useState(null);
  const [selectedVehicle, setSelectedVehicle] = useState(null);
  const [selectedCompartment, setSelectedCompartment] = useState(null);

  useEffect(() => {
    loadAllData();
  }, []);

  const loadAllData = async () => {
    setLoading(true);
    try {
      const [vehiclesData, itemsData, compartmentsData] = await Promise.all([
        api.getVehicles().catch(() => []),
        api.getItems().catch(() => []),
        api.getCompartments().catch(() => []),
      ]);
      
      setVehicles(Array.isArray(vehiclesData) ? vehiclesData : []);
      setItems(Array.isArray(itemsData) ? itemsData : []);
      setCompartments(Array.isArray(compartmentsData) ? compartmentsData : []);
      
      if (vehiclesData.length > 0) setSelectedVehicle(vehiclesData[0]);
      
      showSnackbar('Dane załadowane pomyślnie', 'success');
    } catch (error) {
      console.error('Error loading data:', error);
      showSnackbar('Błąd połączenia z serwerem. Używam danych testowych.', 'warning');
      
      const testVehicles = [
        { id: 1, registrationNumber: 'WX 12345', make: 'Mercedes', model: 'Sprinter', status: 'Active', vehicleType: 'Ambulance' },
        { id: 2, registrationNumber: 'WX 54321', make: 'Volkswagen', model: 'Crafter', status: 'Active', vehicleType: 'Van' },
      ];
      const testCompartments = [
        { id: 1, name: 'Kompartment lewy 1', location: 'Left Front', vehicleId: 1, capacity: 10 },
        { id: 2, name: 'Kompartment lewy 2', location: 'Left Middle', vehicleId: 1, capacity: 8 },
        { id: 3, name: 'Kompartment lewy 3', location: 'Left Rear', vehicleId: 1, capacity: 6 },
        { id: 4, name: 'Kompartment prawy 1', location: 'Right Front', vehicleId: 1, capacity: 9 },
      ];
      const testItems = [
        { id: 1, name: 'Defibrylator', serialNumber: 'DEF-001', status: 'Available', category: 'Medical', compartmentId: 1 },
        { id: 2, name: 'Apteczka', serialNumber: 'APT-002', status: 'Available', category: 'Medical', compartmentId: 1 },
      ];
      
      setVehicles(testVehicles);
      setCompartments(testCompartments);
      setItems(testItems);
      setSelectedVehicle(testVehicles[0]);
    } finally {
      setLoading(false);
    }
  };

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  const handleViewChange = (view) => {
    setCurrentView(view);
    setMobileOpen(false);
  };

  const showSnackbar = (message, severity = 'success') => {
    setSnackbar({ open: true, message, severity });
  };

  const handleCloseSnackbar = () => {
    setSnackbar({ ...snackbar, open: false });
  };

  const handleCreateVehicle = async (vehicleData) => {
    try {
      const newVehicle = await api.createVehicle(vehicleData);
      setVehicles([...vehicles, newVehicle]);
      setVehicleAddOpen(false);
      showSnackbar('Pojazd dodany pomyślnie', 'success');
    } catch (error) {
      showSnackbar('Błąd podczas dodawania pojazdu', 'error');
    }
  };

  const handleEditVehicle = (vehicle) => {
    setEditingVehicle(vehicle);
    setVehicleEditOpen(true);
  };

  const handleSaveVehicle = async (vehicleData) => {
    if (!editingVehicle) return;
    try {
      const updated = await api.updateVehicle(editingVehicle.id, vehicleData);
      setVehicles(vehicles.map(v => v.id === editingVehicle.id ? updated : v));
      setVehicleEditOpen(false);
      setEditingVehicle(null);
      showSnackbar('Pojazd zaktualizowany pomyślnie', 'success');
    } catch (error) {
      showSnackbar('Błąd podczas aktualizacji pojazdu', 'error');
    }
  };

  const handleDeleteVehicle = async (id) => {
    if (window.confirm('Czy na pewno chcesz usunąć ten pojazd?')) {
      try {
        await api.deleteVehicle(id);
        setVehicles(vehicles.filter(v => v.id !== id));
        showSnackbar('Pojazd usunięty pomyślnie', 'success');
      } catch (error) {
        showSnackbar('Błąd podczas usuwania pojazdu', 'error');
      }
    }
  };

  const handleDeleteItem = async (id) => {
    if (window.confirm('Czy na pewno chcesz usunąć ten przedmiot?')) {
      try {
        await api.deleteItem(id);
        setItems(items.filter(i => i.id !== id));
        showSnackbar('Przedmiot usunięty pomyślnie', 'success');
      } catch (error) {
        showSnackbar('Błąd podczas usuwania przedmiotu', 'error');
      }
    }
  };

  const handleDeleteCompartment = async (id) => {
    if (window.confirm('Czy na pewno chcesz usunąć ten kompartment?')) {
      try {
        await api.deleteCompartment(id);
        setCompartments(compartments.filter(c => c.id !== id));
        showSnackbar('Kompartment usunięty pomyślnie', 'success');
      } catch (error) {
        showSnackbar('Błąd podczas usuwania kompartmentu', 'error');
      }
    }
  };

  const stats = {
    totalVehicles: Array.isArray(vehicles) ? vehicles.length : 0,
    activeVehicles: Array.isArray(vehicles) ? vehicles.filter(v => v.status === 'Active').length : 0,
    totalItems: Array.isArray(items) ? items.length : 0,
    availableItems: Array.isArray(items) ? items.filter(i => i.status === 'Available').length : 0,
    totalCompartments: Array.isArray(compartments) ? compartments.length : 0,
  };

  const menuItems = [
    { text: 'Panel główny', icon: <DashboardIcon />, view: 'dashboard' },
    { text: 'Samochody', icon: <DirectionsCarIcon />, view: 'vehicles' },
    { text: 'Sprzęt', icon: <InventoryIcon />, view: 'items' },
    { text: 'Skrytki', icon: <WorkIcon />, view: 'compartments' },
  ];

  const drawer = (
    <Box sx={{ height: '100%', background: 'linear-gradient(180deg, #13186e 0%, #1a1f8e 100%)' }}>
      <Toolbar sx={{ display: 'flex', alignItems: 'center', gap: 1, py: 2 }}>
        <LocalFireDepartmentIcon sx={{ color: '#d21919', fontSize: 32 }} />
        <Typography variant="h6" sx={{ color: 'white', fontWeight: 700 }}>
          Fire Admin
        </Typography>
      </Toolbar>
      <Divider sx={{ bgcolor: 'rgba(255,255,255,0.1)' }} />
      <List sx={{ px: 2, pt: 2 }}>
        {menuItems.map((item) => (
          <ListItem key={item.text} disablePadding sx={{ mb: 1 }}>
            <ListItemButton
              selected={currentView === item.view}
              onClick={() => handleViewChange(item.view)}
              sx={{
                borderRadius: 2,
                color: 'rgba(255,255,255,0.7)',
                '&.Mui-selected': {
                  bgcolor: 'rgba(210, 25, 25, 0.2)',
                  color: 'white',
                  '&:hover': {
                    bgcolor: 'rgba(210, 25, 25, 0.3)',
                  }
                },
                '&:hover': {
                  bgcolor: 'rgba(255,255,255,0.05)',
                }
              }}
            >
              <ListItemIcon sx={{ color: currentView === item.view ? '#d21919' : 'rgba(255,255,255,0.7)', minWidth: 40 }}>
                {item.icon}
              </ListItemIcon>
              <ListItemText 
                primary={item.text} 
                primaryTypographyProps={{ fontWeight: currentView === item.view ? 600 : 400 }}
              />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Box>
  );

  const DashboardView = () => (
    <Fade in={true}>
      <Box>
        <Box sx={{ mb: 4, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Box>
            <Typography variant="h4" fontWeight={700} gutterBottom>
              Dashboard
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Witaj w systemie zarządzania flotą
            </Typography>
          </Box>
          <IconButton 
            onClick={loadAllData} 
            color="primary" 
            disabled={loading}
            sx={{ 
              bgcolor: 'primary.main', 
              color: 'white',
              '&:hover': { bgcolor: 'primary.dark' }
            }}
          >
            <RefreshIcon />
          </IconButton>
        </Box>

        {loading ? (
          <Box display="flex" justifyContent="center" p={8}>
            <CircularProgress size={60} />
          </Box>
        ) : (
          <>
            <Grid container spacing={3} sx={{ mb: 4 }}>
              <Grid item xs={12} sm={6} md={3}>
                <Card sx={{ height: '100%' }}>
                  <CardContent>
                    <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mb: 2 }}>
                      <Avatar sx={{ bgcolor: 'primary.main', width: 56, height: 56 }}>
                        <DirectionsCarIcon sx={{ fontSize: 28 }} />
                      </Avatar>
                      <Chip label="+12%" size="small" color="success" icon={<TrendingUpIcon />} />
                    </Box>
                    <Typography color="text.secondary" variant="body2" gutterBottom>
                      Pojazdy
                    </Typography>
                    <Typography variant="h3" fontWeight={700}>{stats.totalVehicles}</Typography>
                    <Typography variant="body2" color="success.main" sx={{ mt: 1 }}>
                      Aktywne: {stats.activeVehicles}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>

              <Grid item xs={12} sm={6} md={3}>
                <Card sx={{ height: '100%' }}>
                  <CardContent>
                    <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mb: 2 }}>
                      <Avatar sx={{ bgcolor: 'secondary.main', width: 56, height: 56 }}>
                        <InventoryIcon sx={{ fontSize: 28 }} />
                      </Avatar>
                      <Chip label="+8%" size="small" color="success" icon={<TrendingUpIcon />} />
                    </Box>
                    <Typography color="text.secondary" variant="body2" gutterBottom>
                      Przedmioty
                    </Typography>
                    <Typography variant="h3" fontWeight={700}>{stats.totalItems}</Typography>
                    <Typography variant="body2" color="success.main" sx={{ mt: 1 }}>
                      Dostępne: {stats.availableItems}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>

              <Grid item xs={12} sm={6} md={3}>
                <Card sx={{ height: '100%' }}>
                  <CardContent>
                    <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mb: 2 }}>
                      <Avatar sx={{ bgcolor: 'warning.main', width: 56, height: 56 }}>
                        <WorkIcon sx={{ fontSize: 28 }} />
                      </Avatar>
                    </Box>
                    <Typography color="text.secondary" variant="body2" gutterBottom>
                      Kompartmenty
                    </Typography>
                    <Typography variant="h3" fontWeight={700}>{stats.totalCompartments}</Typography>
                    <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
                      Wszystkie aktywne
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>

              <Grid item xs={12} sm={6} md={3}>
                <Card sx={{ height: '100%' }}>
                  <CardContent>
                    <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mb: 2 }}>
                      <Avatar sx={{ bgcolor: 'info.main', width: 56, height: 56 }}>
                        <CheckCircleIcon sx={{ fontSize: 28 }} />
                      </Avatar>
                    </Box>
                    <Typography color="text.secondary" variant="body2" gutterBottom>
                      Gotowość
                    </Typography>
                    <Typography variant="h3" fontWeight={700}>
                      {stats.totalVehicles > 0 ? Math.round((stats.activeVehicles / stats.totalVehicles) * 100) : 0}%
                    </Typography>
                    <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
                      Pojazdy w gotowości
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>
            </Grid>

            {/* Wizualizacja pojazdu */}
            <Box sx={{ mb: 4 }}>
              <Typography variant="h5" fontWeight={700} sx={{ mb: 3 }}>
                Wizualizacja pojazdu
              </Typography>
              
              <Grid container spacing={3} sx={{ mb: 3 }}>
                {vehicles.map((vehicle) => (
                  <Grid item xs={12} sm={6} md={4} key={vehicle.id}>
                    <Card 
                      sx={{ 
                        cursor: 'pointer',
                        border: selectedVehicle?.id === vehicle.id ? 2 : 0,
                        borderColor: 'primary.main',
                      }}
                    >
                      <CardActionArea onClick={() => setSelectedVehicle(vehicle)}>
                        <CardContent>
                          <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                            <DirectionsCarIcon sx={{ fontSize: 40, color: 'primary.main', mr: 2 }} />
                            <Box>
                              <Typography variant="h6" fontWeight={600}>
                                {vehicle.make} {vehicle.model}
                              </Typography>
                              <Typography variant="body2" color="text.secondary">
                                {vehicle.registrationNumber}
                              </Typography>
                            </Box>
                          </Box>
                          <Chip 
                            label={vehicle.status}
                            size="small"
                            color={vehicle.status === 'Active' ? 'success' : vehicle.status === 'Maintenance' ? 'warning' : 'default'}
                            icon={vehicle.status === 'Active' ? <CheckCircleIcon /> : vehicle.status === 'Maintenance' ? <BuildIcon /> : <PauseCircleIcon />}
                          />
                        </CardContent>
                      </CardActionArea>
                    </Card>
                  </Grid>
                ))}
              </Grid>

              {selectedVehicle && (
                <VehicleVisualization 
                  vehicle={selectedVehicle}
                  compartments={compartments.filter(c => c.vehicleId === selectedVehicle.id)}
                  onCompartmentClick={(comp) => {
                    setSelectedCompartment(comp);
                    setCurrentView('compartments');
                  }}
                />
              )}
            </Box>
          </>
        )}
      </Box>
    </Fade>
  );

  const VehiclesView = () => (
    <Fade in={true}>
      <Box>
        <Box sx={{ mb: 4, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Box>
            <Typography variant="h4" fontWeight={700} gutterBottom>
              Pojazdy
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Zarządzaj flotą pojazdów
            </Typography>
          </Box>
          <Box sx={{ display: 'flex', gap: 1 }}>
            <IconButton 
              onClick={loadAllData} 
              color="primary" 
              disabled={loading}
              sx={{ bgcolor: 'grey.100' }}
            >
              <RefreshIcon />
            </IconButton>
            <Button
              variant="contained"
              startIcon={<AddIcon />}
              onClick={() => setVehicleAddOpen(true)}
              sx={{ borderRadius: 2 }}
            >
              Dodaj pojazd
            </Button>
          </Box>
        </Box>

        {loading ? (
          <Box display="flex" justifyContent="center" p={8}>
            <CircularProgress size={60} />
          </Box>
        ) : (
          <TableContainer component={Paper} sx={{ borderRadius: 3 }}>
            <Table>
              <TableHead sx={{ bgcolor: 'grey.50' }}>
                <TableRow>
                  <TableCell sx={{ fontWeight: 700 }}>Nr rejestracyjny</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Marka</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Model</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Typ</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Status</TableCell>
                  <TableCell align="right" sx={{ fontWeight: 700 }}>Akcje</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {vehicles.map((vehicle) => (
                  <TableRow 
                    key={vehicle.id}
                    sx={{ '&:hover': { bgcolor: 'grey.50' } }}
                  >
                    <TableCell>
                      <Typography fontWeight={600}>{vehicle.registrationNumber}</Typography>
                    </TableCell>
                    <TableCell>{vehicle.make}</TableCell>
                    <TableCell>{vehicle.model}</TableCell>
                    <TableCell>
                      <Chip label={vehicle.vehicleType} size="small" variant="outlined" />
                    </TableCell>
                    <TableCell>
                      <Chip
                        label={vehicle.status}
                        color={vehicle.status === 'Active' ? 'success' : vehicle.status === 'Maintenance' ? 'warning' : 'default'}
                        size="small"
                        icon={vehicle.status === 'Active' ? <CheckCircleIcon /> : vehicle.status === 'Maintenance' ? <BuildIcon /> : <PauseCircleIcon />}
                      />
                    </TableCell>
                    <TableCell align="right">
                      <Tooltip title="Zobacz">
                        <IconButton 
                          size="small" 
                          color="primary"
                          onClick={() => {
                            setSelectedVehicle(vehicle);
                            setCurrentView('dashboard');
                          }}
                        >
                          <VisibilityIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Edytuj">
                        <IconButton 
                          size="small" 
                          color="primary"
                          onClick={() => handleEditVehicle(vehicle)}
                        >
                          <EditIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Usuń">
                        <IconButton 
                          size="small" 
                          color="error"
                          onClick={() => handleDeleteVehicle(vehicle.id)}
                        >
                          <DeleteIcon />
                        </IconButton>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        )}
      </Box>
    </Fade>
  );

  const ItemsView = () => (
    <Fade in={true}>
      <Box>
        <Box sx={{ mb: 4, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Box>
            <Typography variant="h4" fontWeight={700} gutterBottom>
              Przedmioty
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Zarządzaj wyposażeniem
            </Typography>
          </Box>
          <Box sx={{ display: 'flex', gap: 1 }}>
            <IconButton 
              onClick={loadAllData} 
              color="primary" 
              disabled={loading}
              sx={{ bgcolor: 'grey.100' }}
            >
              <RefreshIcon />
            </IconButton>
            <Button
              variant="contained"
              startIcon={<AddIcon />}
              onClick={() => showSnackbar('Formularz dodawania będzie wkrótce', 'info')}
              sx={{ borderRadius: 2 }}
            >
              Dodaj przedmiot
            </Button>
          </Box>
        </Box>

        {loading ? (
          <Box display="flex" justifyContent="center" p={8}>
            <CircularProgress size={60} />
          </Box>
        ) : (
          <TableContainer component={Paper} sx={{ borderRadius: 3 }}>
            <Table>
              <TableHead sx={{ bgcolor: 'grey.50' }}>
                <TableRow>
                  <TableCell sx={{ fontWeight: 700 }}>Nazwa</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Numer seryjny</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Kategoria</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Status</TableCell>
                  <TableCell sx={{ fontWeight: 700 }}>Kompartment</TableCell>
                  <TableCell align="right" sx={{ fontWeight: 700 }}>Akcje</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {items.map((item) => (
                  <TableRow 
                    key={item.id}
                    sx={{ '&:hover': { bgcolor: 'grey.50' } }}
                  >
                    <TableCell>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <InventoryIcon sx={{ color: 'text.secondary', fontSize: 20 }} />
                        <Typography fontWeight={600}>{item.name}</Typography>
                      </Box>
                    </TableCell>
                    <TableCell>{item.serialNumber}</TableCell>
                    <TableCell>
                      <Chip label={item.category} size="small" variant="outlined" />
                    </TableCell>
                    <TableCell>
                      <Chip
                        label={item.status}
                        color={item.status === 'Available' ? 'success' : 'warning'}
                        size="small"
                      />
                    </TableCell>
                    <TableCell>{item.compartmentId}</TableCell>
                    <TableCell align="right">
                      <Tooltip title="Zobacz">
                        <IconButton size="small" color="primary">
                          <VisibilityIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Edytuj">
                        <IconButton size="small" color="primary">
                          <EditIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Usuń">
                        <IconButton 
                          size="small" 
                          color="error"
                          onClick={() => handleDeleteItem(item.id)}
                        >
                          <DeleteIcon />
                        </IconButton>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        )}
      </Box>
    </Fade>
  );

  const CompartmentsView = () => (
    <Fade in={true}>
      <Box>
        <Box sx={{ mb: 4, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Box>
            <Typography variant="h4" fontWeight={700} gutterBottom>
              Kompartmenty
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Zarządzaj skrytkami pojazdów
            </Typography>
          </Box>
          <Box sx={{ display: 'flex', gap: 1 }}>
            <IconButton 
              onClick={loadAllData} 
              color="primary" 
              disabled={loading}
              sx={{ bgcolor: 'grey.100' }}
            >
              <RefreshIcon />
            </IconButton>
            <Button
              variant="contained"
              startIcon={<AddIcon />}
              onClick={() => showSnackbar('Formularz dodawania będzie wkrótce', 'info')}
              sx={{ borderRadius: 2 }}
            >
              Dodaj kompartment
            </Button>
          </Box>
        </Box>

        {loading ? (
          <Box display="flex" justifyContent="center" p={8}>
            <CircularProgress size={60} />
          </Box>
        ) : (
          <>
            {selectedCompartment && (
              <Paper sx={{ p: 3, mb: 3, borderRadius: 3, bgcolor: 'primary.main', color: 'white' }}>
                <Typography variant="h6" fontWeight={700} gutterBottom>
                  Szczegóły wybranego kompartmentu
                </Typography>
                <Typography variant="body1">
                  {selectedCompartment.name} - Lokalizacja: {selectedCompartment.location}
                </Typography>
                <Typography variant="body2" sx={{ mt: 1, opacity: 0.9 }}>
                  Pojemność: {selectedCompartment.capacity} przedmiotów
                </Typography>
                <Button 
                  variant="outlined" 
                  sx={{ mt: 2, color: 'white', borderColor: 'white' }}
                  onClick={() => setSelectedCompartment(null)}
                >
                  Zamknij szczegóły
                </Button>
              </Paper>
            )}
            
            <TableContainer component={Paper} sx={{ borderRadius: 3 }}>
              <Table>
                <TableHead sx={{ bgcolor: 'grey.50' }}>
                  <TableRow>
                    <TableCell sx={{ fontWeight: 700 }}>Nazwa</TableCell>
                    <TableCell sx={{ fontWeight: 700 }}>Lokalizacja</TableCell>
                    <TableCell sx={{ fontWeight: 700 }}>Pojazd ID</TableCell>
                    <TableCell sx={{ fontWeight: 700 }}>Pojemność</TableCell>
                    <TableCell align="right" sx={{ fontWeight: 700 }}>Akcje</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {compartments.map((compartment) => (
                    <TableRow 
                      key={compartment.id}
                      sx={{ 
                        '&:hover': { bgcolor: 'grey.50' },
                        bgcolor: selectedCompartment?.id === compartment.id ? 'primary.lighter' : 'transparent'
                      }}
                    >
                      <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                          <WorkIcon sx={{ color: 'text.secondary', fontSize: 20 }} />
                          <Typography fontWeight={600}>{compartment.name}</Typography>
                        </Box>
                      </TableCell>
                      <TableCell>{compartment.location}</TableCell>
                      <TableCell>
                        <Chip label={`Pojazd #${compartment.vehicleId}`} size="small" variant="outlined" />
                      </TableCell>
                      <TableCell>
                        <Chip 
                          label={`${compartment.capacity} szt.`} 
                          size="small" 
                          color="info"
                        />
                      </TableCell>
                      <TableCell align="right">
                        <Tooltip title="Zobacz">
                          <IconButton 
                            size="small" 
                            color="primary"
                            onClick={() => setSelectedCompartment(compartment)}
                          >
                            <VisibilityIcon />
                          </IconButton>
                        </Tooltip>
                        <Tooltip title="Edytuj">
                          <IconButton size="small" color="primary">
                            <EditIcon />
                          </IconButton>
                        </Tooltip>
                        <Tooltip title="Usuń">
                          <IconButton 
                            size="small" 
                            color="error"
                            onClick={() => handleDeleteCompartment(compartment.id)}
                          >
                            <DeleteIcon />
                          </IconButton>
                        </Tooltip>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </>
        )}
      </Box>
    </Fade>
  );

  const renderView = () => {
    switch (currentView) {
      case 'dashboard':
        return <DashboardView />;
      case 'vehicles':
        return <VehiclesView />;
      case 'items':
        return <ItemsView />;
      case 'compartments':
        return <CompartmentsView />;
      default:
        return <DashboardView />;
    }
  };

  return (
    <ThemeProvider theme={theme}>
      <Box sx={{ display: 'flex' }}>
        <CssBaseline />
        <AppBar
          position="fixed"
          elevation={0}
          sx={{
            width: { sm: `calc(100% - ${drawerWidth}px)` },
            ml: { sm: `${drawerWidth}px` },
            bgcolor: 'white',
            color: 'text.primary',
            borderBottom: '1px solid',
            borderColor: 'divider',
          }}
        >
          <Toolbar>
            <IconButton
              color="inherit"
              edge="start"
              onClick={handleDrawerToggle}
              sx={{ mr: 2, display: { sm: 'none' } }}
            >
              <MenuIcon />
            </IconButton>
            <Box sx={{ display: 'flex', alignItems: 'center', gap: 2, flexGrow: 1 }}>
              <ViewInArIcon sx={{ color: 'primary.main', fontSize: 28 }} />
              <Typography variant="h6" noWrap fontWeight={700}>
                System Zarządzania Pojazdami
              </Typography>
            </Box>
            <Badge badgeContent={stats.activeVehicles} color="success">
              <DirectionsCarIcon />
            </Badge>
          </Toolbar>
        </AppBar>
        <Box
          component="nav"
          sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }}
        >
          <Drawer
            variant="temporary"
            open={mobileOpen}
            onClose={handleDrawerToggle}
            ModalProps={{ keepMounted: true }}
            sx={{
              display: { xs: 'block', sm: 'none' },
              '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
            }}
          >
            {drawer}
          </Drawer>
          <Drawer
            variant="permanent"
            sx={{
              display: { xs: 'none', sm: 'block' },
              '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
            }}
            open
          >
            {drawer}
          </Drawer>
        </Box>
        <Box
          component="main"
          sx={{
            flexGrow: 1,
            p: 3,
            width: { sm: `calc(100% - ${drawerWidth}px)` },
            minHeight: '100vh',
            bgcolor: 'background.default',
          }}
        >
          <Toolbar />
          <Box sx={{ mt: 2 }}>
            {renderView()}
          </Box>
        </Box>

        <VehicleAddDialog
          open={vehicleAddOpen}
          onClose={() => setVehicleAddOpen(false)}
          onSave={handleCreateVehicle}
        />

        <VehicleEditDialog
          open={vehicleEditOpen}
          vehicle={editingVehicle}
          onClose={() => {
            setVehicleEditOpen(false);
            setEditingVehicle(null);
          }}
          onSave={handleSaveVehicle}
        />

        <Snackbar
          open={snackbar.open}
          autoHideDuration={6000}
          onClose={handleCloseSnackbar}
          anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        >
          <Alert 
            onClose={handleCloseSnackbar} 
            severity={snackbar.severity}
            variant="filled"
            sx={{ borderRadius: 2 }}
          >
            {snackbar.message}
          </Alert>
        </Snackbar>
      </Box>
    </ThemeProvider>
  );
}