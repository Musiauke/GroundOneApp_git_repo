import React, { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  MenuItem,
  Box,
  Typography,
} from '@mui/material';
import { VEHICLE_TYPES, VEHICLE_STATUSES } from '../../config/constants';

export default function VehicleAddDialog({ open, onClose, onSave }) {
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
            {VEHICLE_TYPES.map((type) => (
              <MenuItem key={type.value} value={type.value}>
                {type.label}
              </MenuItem>
            ))}
          </TextField>
          <TextField
            select
            label="Status"
            value={formData.status}
            onChange={handleChange('status')}
            fullWidth
            required
          >
            {VEHICLE_STATUSES.map((status) => (
              <MenuItem key={status.value} value={status.value}>
                {status.label}
              </MenuItem>
            ))}
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