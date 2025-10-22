// frontend/src/components/VehicleForm.jsx
import React, { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  MenuItem,
} from '@mui/material';

export default function VehicleForm({ open, onClose, onSubmit }) {
  const [form, setForm] = useState({
    registrationNumber: '',
    make: '',
    model: '',
    vehicleType: 'Ambulance',
    status: 'Active',
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = () => {
    onSubmit(form);
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>Dodaj pojazd</DialogTitle>
      <DialogContent dividers>
        <TextField
          margin="dense"
          label="Nr rejestracyjny"
          name="registrationNumber"
          fullWidth
          value={form.registrationNumber}
          onChange={handleChange}
        />
        <TextField
          margin="dense"
          label="Marka"
          name="make"
          fullWidth
          value={form.make}
          onChange={handleChange}
        />
        <TextField
          margin="dense"
          label="Model"
          name="model"
          fullWidth
          value={form.model}
          onChange={handleChange}
        />
        <TextField
          select
          margin="dense"
          label="Typ pojazdu"
          name="vehicleType"
          fullWidth
          value={form.vehicleType}
          onChange={handleChange}
        >
          <MenuItem value="Ambulance">Ambulans</MenuItem>
          <MenuItem value="FireTruck">Wóz Strażacki</MenuItem>
          <MenuItem value="PoliceCar">Radiowóz</MenuItem>
        </TextField>
        <TextField
          select
          margin="dense"
          label="Status"
          name="status"
          fullWidth
          value={form.status}
          onChange={handleChange}
        >
          <MenuItem value="Active">Aktywny</MenuItem>
          <MenuItem value="Inactive">Nieaktywny</MenuItem>
        </TextField>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Anuluj</Button>
        <Button variant="contained" onClick={handleSubmit}>Zapisz</Button>
      </DialogActions>
    </Dialog>
  );
}