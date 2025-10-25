// dashboard view
import React from 'react';
import {
  Box,
  Typography,
  IconButton,
  CircularProgress,
  Grid,
  Card,
  CardContent,
  Avatar,
  Chip,
  Fade,
  CardActionArea,
} from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import DirectionsCarIcon from '@mui/icons-material/DirectionsCar';
import InventoryIcon from '@mui/icons-material/Inventory';
import WorkIcon from '@mui/icons-material/Work';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import TrendingUpIcon from '@mui/icons-material/TrendingUp';
import BuildIcon from '@mui/icons-material/Build';
import PauseCircleIcon from '@mui/icons-material/PauseCircle';
import VehicleVisualization from '../vehicle/VehicleVisualization';

export default function Dashboard({ 
  loading, 
  stats, 
  vehicles, 
  compartments, 
  selectedVehicle, 
  onRefresh, 
  onVehicleSelect,
  onCompartmentClick 
}) {
  return (
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
            onClick={onRefresh} 
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
                      <CardActionArea onClick={() => onVehicleSelect(vehicle)}>
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
                  onCompartmentClick={onCompartmentClick}
                />
              )}
            </Box>
          </>
        )}
      </Box>
    </Fade>
  );
}