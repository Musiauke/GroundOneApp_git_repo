import React, { useState, useEffect } from 'react';
import {
  Box,
  Drawer,
  AppBar,
  Toolbar,
  Typography,
  CssBaseline,
  ThemeProvider,
  IconButton,
  Badge,
  Snackbar,
  Alert,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import DirectionsCarIcon from '@mui/icons-material/DirectionsCar';
import ViewInArIcon from '@mui/icons-material/ViewInAr';
import theme from '../../theme/theme';
import api from '../../services/api';
import { DRAWER_WIDTH, MENU_ITEMS } from '../../config/constants';
import Sidebar from '../layout/Sidebar';
import Dashboard from './Dashboard';
import VehiclesView from './VehiclesView';
import ItemsView from './ItemsView';
import CompartmentsView from './CompartmentView';
import VehicleAddDialog from '../dialogs/VehicleAddDialog';
import VehicleEditDialog from '../dialogs/VehicleEditDialog';

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

  const renderView = () => {
    switch (currentView) {
      case 'dashboard':
        return (
          <Dashboard
            loading={loading}
            stats={stats}
            vehicles={vehicles}
            compartments={compartments}
            selectedVehicle={selectedVehicle}
            onRefresh={loadAllData}
            onVehicleSelect={setSelectedVehicle}
            onCompartmentClick={(comp) => {
              setSelectedCompartment(comp);
              setCurrentView('compartments');
            }}
          />
        );
      case 'vehicles':
        return (
          <VehiclesView
            loading={loading}
            vehicles={vehicles}
            onRefresh={loadAllData}
            onAdd={() => setVehicleAddOpen(true)}
            onEdit={handleEditVehicle}
            onDelete={handleDeleteVehicle}
            onView={(vehicle) => {
              setSelectedVehicle(vehicle);
              setCurrentView('dashboard');
            }}
          />
        );
      case 'items':
        return (
          <ItemsView
            loading={loading}
            items={items}
            onRefresh={loadAllData}
            onAdd={() => showSnackbar('Formularz dodawania będzie wkrótce', 'info')}
            onDelete={handleDeleteItem}
          />
        );
      case 'compartments':
        return (
          <CompartmentsView
            loading={loading}
            compartments={compartments}
            selectedCompartment={selectedCompartment}
            onRefresh={loadAllData}
            onAdd={() => showSnackbar('Formularz dodawania będzie wkrótce', 'info')}
            onDelete={handleDeleteCompartment}
            onView={setSelectedCompartment}
            onCloseDetails={() => setSelectedCompartment(null)}
          />
        );
      default:
        return <Dashboard />;
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
            width: { sm: `calc(100% - ${DRAWER_WIDTH}px)` },
            ml: { sm: `${DRAWER_WIDTH}px` },
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
          sx={{ width: { sm: DRAWER_WIDTH }, flexShrink: { sm: 0 } }}
        >
          <Drawer
            variant="temporary"
            open={mobileOpen}
            onClose={handleDrawerToggle}
            ModalProps={{ keepMounted: true }}
            sx={{
              display: { xs: 'block', sm: 'none' },
              '& .MuiDrawer-paper': { boxSizing: 'border-box', width: DRAWER_WIDTH },
            }}
          >
            <Sidebar 
              currentView={currentView} 
              onViewChange={handleViewChange}
              menuItems={MENU_ITEMS}
            />
          </Drawer>
          <Drawer
            variant="permanent"
            sx={{
              display: { xs: 'none', sm: 'block' },
              '& .MuiDrawer-paper': { boxSizing: 'border-box', width: DRAWER_WIDTH },
            }}
            open
          >
            <Sidebar 
              currentView={currentView} 
              onViewChange={handleViewChange}
              menuItems={MENU_ITEMS}
            />
          </Drawer>
        </Box>
        <Box
          component="main"
          sx={{
            flexGrow: 1,
            p: 3,
            width: { sm: `calc(100% - ${DRAWER_WIDTH}px)` },
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