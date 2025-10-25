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
import WorkIcon from '@mui/icons-material/Work';

export default function CompartmentsView({ 
  loading, 
  compartments,
  selectedCompartment,
  onRefresh, 
  onAdd, 
  onDelete,
  onView,
  onCloseDetails
}) {
  return (
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
                  onClick={onCloseDetails}
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
                            onClick={() => onView(compartment)}
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
                            onClick={() => onDelete(compartment.id)}
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
}