import React from 'react';
import {
  Box,
  Typography,
  IconButton,
  Button,
  CircularProgress,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Paper,
  Chip,
  Tooltip,
  Fade,
} from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import VisibilityIcon from '@mui/icons-material/Visibility';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import BuildIcon from '@mui/icons-material/Build';
import PauseCircleIcon from '@mui/icons-material/PauseCircle';

export default function VehiclesView({ 
  loading, 
  vehicles, 
  onRefresh, 
  onAdd, 
  onEdit, 
  onDelete,
  onView 
}) {
  return (
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
              onClick={onRefresh} 
              color="primary" 
              disabled={loading}
              sx={{ bgcolor: 'grey.100' }}
            >
              <RefreshIcon />
            </IconButton>
            <Button
              variant="contained"
              startIcon={<AddIcon />}
              onClick={onAdd}
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
                          onClick={() => onView(vehicle)}
                        >
                          <VisibilityIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Edytuj">
                        <IconButton 
                          size="small" 
                          color="primary"
                          onClick={() => onEdit(vehicle)}
                        >
                          <EditIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Usuń">
                        <IconButton 
                          size="small" 
                          color="error"
                          onClick={() => onDelete(vehicle.id)}
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
}