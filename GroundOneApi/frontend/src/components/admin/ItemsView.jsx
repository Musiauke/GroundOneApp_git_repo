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
import InventoryIcon from '@mui/icons-material/Inventory';

export default function ItemsView({ 
  loading, 
  items, 
  onRefresh, 
  onAdd, 
  onDelete 
}) {
  return (
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
                          onClick={() => onDelete(item.id)}
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