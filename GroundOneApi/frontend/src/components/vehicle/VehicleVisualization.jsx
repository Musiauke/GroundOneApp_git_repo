import React, { useState } from 'react';
import {
  Box,
  Paper,
  Typography,
  ButtonGroup,
  Button,
  Card,
  CardContent,
  Zoom,
  Tooltip,
} from '@mui/material';
import WorkIcon from '@mui/icons-material/Work';

function VehicleVisualization({ vehicle, compartments, onCompartmentClick }) {
  const [currentView, setCurrentView] = useState('left');

  const getCompartmentsByView = (view) => {
    return compartments.filter(c => {
      if (view === 'left') return c.location?.toLowerCase().includes('left') || c.location?.toLowerCase().includes('lew');
      if (view === 'right') return c.location?.toLowerCase().includes('right') || c.location?.toLowerCase().includes('praw');
      if (view === 'rear') return c.location?.toLowerCase().includes('rear') || c.location?.toLowerCase().includes('tyÅ‚');
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

export default VehicleVisualization;