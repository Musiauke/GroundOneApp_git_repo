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
  CircularProgress
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

const drawerWidth = 240;

const theme = createTheme({
  palette: {
    primary: {
      main: '#d21919ff',
    },
    secondary: {
      main: '#13186eff',
    },
  },
});

// Konfiguracja API
const API_BASE_URL = 'http://localhost:5049/api';

// Funkcje API
const api = {
  // Vehicles
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
  
  // Items
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
  
  // Compartments
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

export default function AdminPanel() {
  const [mobileOpen, setMobileOpen] = useState(false);
  const [currentView, setCurrentView] = useState('dashboard');
  const [vehicles, setVehicles] = useState([]);
  const [items, setItems] = useState([]);
  const [compartments, setCompartments] = useState([]);
  const [loading, setLoading] = useState(false);
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });

  // Ładowanie danych przy starcie
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
      
      showSnackbar('Dane załadowane pomyślnie', 'success');
    } catch (error) {
      console.error('Error loading data:', error);
      showSnackbar('Błąd połączenia z serwerem. Używam danych testowych.', 'warning');
      // Dane testowe jako fallback
      setVehicles([
        { id: 1, registrationNumber: 'WX 12345', make: 'Mercedes', model: 'Sprinter', status: 'Active', vehicleType: 'Ambulance' },
      ]);
      setItems([
        { id: 1, name: 'Defibrylator', serialNumber: 'DEF-001', status: 'Available', category: 'Medical', compartmentId: 1 },
      ]);
      setCompartments([
        { id: 1, name: 'Kompartment 1', location: 'Left', vehicleId: 1, capacity: 10 },
      ]);
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

  // Dashboard Stats - bezpieczne obliczenia
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
    <div>
      <Toolbar>
        <Typography variant="h6" noWrap component="div">
          Admin Panel
        </Typography>
      </Toolbar>
      <Divider />
      <List>
        {menuItems.map((item) => (
          <ListItem key={item.text} disablePadding>
            <ListItemButton
              selected={currentView === item.view}
              onClick={() => handleViewChange(item.view)}
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </div>
  );

  const DashboardView = () => (
    <Box
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="flex-start"
      minHeight="100vh"
      sx={{ pt: 4 }}
    >
      <Box display="flex" alignItems="center" gap={2} mb={3}>
        <Typography variant="h4">Dashboard</Typography>
        <IconButton onClick={loadAllData} color="primary" disabled={loading}>
          <RefreshIcon />
        </IconButton>
      </Box>

      {loading ? (
        <CircularProgress />
      ) : (
        <Grid container spacing={3} sx={{ mt: 2, maxWidth: "1200px" }}>
          <Grid item xs={12} sm={6} md={3}>
            <Card>
              <CardContent>
                <Typography color="textSecondary" gutterBottom>
                  Pojazdy
                </Typography>
                <Typography variant="h3">{stats.totalVehicles}</Typography>
                <Typography variant="body2" color="textSecondary">
                  Aktywne: {stats.activeVehicles}
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12} sm={6} md={3}>
            <Card>
              <CardContent>
                <Typography color="textSecondary" gutterBottom>
                  Przedmioty
                </Typography>
                <Typography variant="h3">{stats.totalItems}</Typography>
                <Typography variant="body2" color="textSecondary">
                  Dostępne: {stats.availableItems}
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12} sm={6} md={3}>
            <Card>
              <CardContent>
                <Typography color="textSecondary" gutterBottom>
                  Kompartmenty
                </Typography>
                <Typography variant="h3">{stats.totalCompartments}</Typography>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      )}
    </Box>
  );

  const VehiclesView = () => (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">Pojazdy</Typography>
        <Box>
          <IconButton onClick={loadAllData} color="primary" disabled={loading}>
            <RefreshIcon />
          </IconButton>
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            onClick={() => showSnackbar('Formularz dodawania będzie wkrótce', 'info')}
          >
            Dodaj pojazd
          </Button>
        </Box>
      </Box>
      {loading ? (
        <Box display="flex" justifyContent="center" p={4}>
          <CircularProgress />
        </Box>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Nr rejestracyjny</TableCell>
                <TableCell>Marka</TableCell>
                <TableCell>Model</TableCell>
                <TableCell>Typ</TableCell>
                <TableCell>Status</TableCell>
                <TableCell align="right">Akcje</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {vehicles.map((vehicle) => (
                <TableRow key={vehicle.id}>
                  <TableCell>{vehicle.registrationNumber}</TableCell>
                  <TableCell>{vehicle.make}</TableCell>
                  <TableCell>{vehicle.model}</TableCell>
                  <TableCell>{vehicle.vehicleType}</TableCell>
                  <TableCell>
                    <Chip
                      label={vehicle.status}
                      color={vehicle.status === 'Active' ? 'success' : 'default'}
                      size="small"
                    />
                  </TableCell>
                  <TableCell align="right">
                    <IconButton size="small" color="primary">
                      <VisibilityIcon />
                    </IconButton>
                    <IconButton size="small" color="primary">
                      <EditIcon />
                    </IconButton>
                    <IconButton 
                      size="small" 
                      color="error"
                      onClick={() => handleDeleteVehicle(vehicle.id)}
                    >
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Box>
  );

  const ItemsView = () => (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">Przedmioty</Typography>
        <Box>
          <IconButton onClick={loadAllData} color="primary" disabled={loading}>
            <RefreshIcon />
          </IconButton>
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            onClick={() => showSnackbar('Formularz dodawania będzie wkrótce', 'info')}
          >
            Dodaj przedmiot
          </Button>
        </Box>
      </Box>
      {loading ? (
        <Box display="flex" justifyContent="center" p={4}>
          <CircularProgress />
        </Box>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Nazwa</TableCell>
                <TableCell>Numer seryjny</TableCell>
                <TableCell>Kategoria</TableCell>
                <TableCell>Status</TableCell>
                <TableCell>Kompartment</TableCell>
                <TableCell align="right">Akcje</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((item) => (
                <TableRow key={item.id}>
                  <TableCell>{item.name}</TableCell>
                  <TableCell>{item.serialNumber}</TableCell>
                  <TableCell>{item.category}</TableCell>
                  <TableCell>
                    <Chip
                      label={item.status}
                      color={item.status === 'Available' ? 'success' : 'warning'}
                      size="small"
                    />
                  </TableCell>
                  <TableCell>{item.compartmentId}</TableCell>
                  <TableCell align="right">
                    <IconButton size="small" color="primary">
                      <VisibilityIcon />
                    </IconButton>
                    <IconButton size="small" color="primary">
                      <EditIcon />
                    </IconButton>
                    <IconButton 
                      size="small" 
                      color="error"
                      onClick={() => handleDeleteItem(item.id)}
                    >
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Box>
  );

  const CompartmentsView = () => (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">Kompartmenty</Typography>
        <Box>
          <IconButton onClick={loadAllData} color="primary" disabled={loading}>
            <RefreshIcon />
          </IconButton>
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            onClick={() => showSnackbar('Formularz dodawania będzie wkrótce', 'info')}
          >
            Dodaj kompartment
          </Button>
        </Box>
      </Box>
      {loading ? (
        <Box display="flex" justifyContent="center" p={4}>
          <CircularProgress />
        </Box>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Nazwa</TableCell>
                <TableCell>Lokalizacja</TableCell>
                <TableCell>Pojazd ID</TableCell>
                <TableCell>Pojemność</TableCell>
                <TableCell align="right">Akcje</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {compartments.map((compartment) => (
                <TableRow key={compartment.id}>
                  <TableCell>{compartment.name}</TableCell>
                  <TableCell>{compartment.location}</TableCell>
                  <TableCell>{compartment.vehicleId}</TableCell>
                  <TableCell>{compartment.capacity}</TableCell>
                  <TableCell align="right">
                    <IconButton size="small" color="primary">
                      <VisibilityIcon />
                    </IconButton>
                    <IconButton size="small" color="primary">
                      <EditIcon />
                    </IconButton>
                    <IconButton 
                      size="small" 
                      color="error"
                      onClick={() => handleDeleteCompartment(compartment.id)}
                    >
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Box>
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
          sx={{
            width: { sm: `calc(100% - ${drawerWidth}px)` },
            ml: { sm: `${drawerWidth}px` },
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
            <Typography variant="h6" noWrap component="div">
              System Zarządzania Pojazdami
            </Typography>
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
            display: 'flex',
            flexDirection: 'column',
          }}
        >
          <Toolbar />
          <Box
            sx={{
              flexGrow: 1,
              display: currentView === 'dashboard' ? 'flex' : 'block',
              alignItems: currentView === 'dashboard' ? 'center' : 'stretch',
              justifyContent: currentView === 'dashboard' ? 'center' : 'flex-start',
            }}
          >
            {renderView()}
          </Box>
        </Box>

        <Snackbar
          open={snackbar.open}
          autoHideDuration={6000}
          onClose={handleCloseSnackbar}
        >
          <Alert onClose={handleCloseSnackbar} severity={snackbar.severity}>
            {snackbar.message}
          </Alert>
        </Snackbar>
      </Box>
    </ThemeProvider>
  );
}