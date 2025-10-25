import React from 'react';
import {
  Box,
  Toolbar,
  List,
  Typography,
  Divider,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from '@mui/material';
import LocalFireDepartmentIcon from '@mui/icons-material/LocalFireDepartment';
import DashboardIcon from '@mui/icons-material/Dashboard';
import DirectionsCarIcon from '@mui/icons-material/DirectionsCar';
import InventoryIcon from '@mui/icons-material/Inventory';
import WorkIcon from '@mui/icons-material/Work';

const iconMap = {
  Dashboard: DashboardIcon,
  DirectionsCar: DirectionsCarIcon,
  Inventory: InventoryIcon,
  Work: WorkIcon,
};

export default function Sidebar({ currentView, onViewChange, menuItems }) {
  return (
    <Box sx={{ height: '100%', background: 'linear-gradient(180deg, #13186e 0%, #1a1f8e 100%)' }}>
      <Toolbar sx={{ display: 'flex', alignItems: 'center', gap: 1, py: 2 }}>
        <LocalFireDepartmentIcon sx={{ color: '#d21919', fontSize: 32 }} />
        <Typography variant="h6" sx={{ color: 'white', fontWeight: 700 }}>
          Fire Admin
        </Typography>
      </Toolbar>
      <Divider sx={{ bgcolor: 'rgba(255,255,255,0.1)' }} />
      <List sx={{ px: 2, pt: 2 }}>
        {menuItems.map((item) => {
          const IconComponent = iconMap[item.icon];
          return (
            <ListItem key={item.text} disablePadding sx={{ mb: 1 }}>
              <ListItemButton
                selected={currentView === item.view}
                onClick={() => onViewChange(item.view)}
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
                  <IconComponent />
                </ListItemIcon>
                <ListItemText 
                  primary={item.text} 
                  primaryTypographyProps={{ fontWeight: currentView === item.view ? 600 : 400 }}
                />
              </ListItemButton>
            </ListItem>
          );
        })}
      </List>
    </Box>
  );
}