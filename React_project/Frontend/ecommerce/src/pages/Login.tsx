import React, { useState } from 'react';
import { Container, Paper, TextField, Button, Typography, Box, Alert, CircularProgress } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/authContext';
import { authService } from '../Services/authService';

const Login: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    console.log('Attempting login with:', { email, password });
    try {
      const response = await authService.login({ email, password });
      console.log('Login response:', response);
      
      const userData = {
        userId: response.userId,
        email: response.email,
        role: response.role
      };

      login(userData, response.accessToken, response.refreshToken);
      switch (response.role.toLowerCase()) {
      case 'admin':
        navigate('/admin-dashboard');
        break;
      case 'vendor':
        navigate('/vendor-dashboard');
        break;
      case 'customer':
      default:
        navigate('/dashboard');
        break;
      }
    } catch (err: any) {
      console.error('Login error:', err);
      setError(err.response?.data?.description || 'Login failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 8 }}>
      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h4" align="center" gutterBottom>
          Login
        </Typography>
        
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit}>
          <TextField
            fullWidth
            label="Email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            margin="normal"
            required
          />
          <TextField
            fullWidth
            label="Password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            margin="normal"
            required
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            disabled={loading}
          >
            {loading ? <CircularProgress size={24} /> : 'Login'}
          </Button>
          <Button
            fullWidth
            variant="text"
            onClick={() => navigate('/forgot-password')}
            sx={{ mt: 1 }}
          >
            Forgot Password?
          </Button>

          <Button
            fullWidth
            variant="text"
            onClick={() => navigate('/register')}
          >
            Don't have an account? Register
          </Button>
        </Box>
      </Paper>
    </Container>
  );
};

export default Login;
