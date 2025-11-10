import React from 'react';
import { Container, Typography, Paper, Box, Grid, Card, CardContent } from '@mui/material';
import { useAuth } from '../context/authContext';

const AdminDashboard: React.FC = () => {
  const { user } = useAuth();

  return (
    <Container maxWidth="lg" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        Admin Dashboard
      </Typography>
      
      <Grid container spacing={3}>
        <Grid size={{ xs: 12, md: 4 }}>
          <Card>
            <CardContent>
              <Typography variant="h6">Manage Vendors</Typography>
              <Typography variant="body2">Approve/Reject vendor applications</Typography>
            </CardContent>
          </Card>
        </Grid>
        
        <Grid size={{ xs: 12, md: 4 }}>
          <Card>
            <CardContent>
              <Typography variant="h6">Manage Categories</Typography>
              <Typography variant="body2">Add/Edit product categories</Typography>
            </CardContent>
          </Card>
        </Grid>
        
        <Grid size={{ xs: 12, md: 4 }}>
          <Card>
            <CardContent>
              <Typography variant="h6">Analytics</Typography>
              <Typography variant="body2">View sales and user analytics</Typography>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      <Paper elevation={3} sx={{ p: 4, mt: 3 }}>
        <Typography variant="h6">Welcome, {user?.email}!</Typography>
        <Typography variant="body1">Role: {user?.role}</Typography>
        <Typography variant="body1">User ID: {user?.userId}</Typography>
      </Paper>
    </Container>
  );
};

export default AdminDashboard;
