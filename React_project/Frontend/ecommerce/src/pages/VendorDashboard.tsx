import React, { useState } from 'react';
import { useAuth } from '../context/authContext';
import ProductManagement from './productManagement';
import VendorProfileManagement from './VendorProfileManagement';

const VendorDashboard: React.FC = () => {
  const { user } = useAuth();
  const [activeTab, setActiveTab] = useState('overview');

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Vendor Dashboard</h2>

      <ul className="nav nav-tabs mb-4">
        <li className="nav-item">
          <button 
            className={`nav-link ${activeTab === 'overview' ? 'active' : ''}`}
            onClick={() => setActiveTab('overview')}
          >
            Overview
          </button>
        </li>
        <li className="nav-item">
          <button 
            className={`nav-link ${activeTab === 'products' ? 'active' : ''}`}
            onClick={() => setActiveTab('products')}
          >
            Products
          </button>
        </li>
        <li className="nav-item">
          <button 
            className={`nav-link ${activeTab === 'orders' ? 'active' : ''}`}
            onClick={() => setActiveTab('orders')}
          >
            Orders
          </button>
        </li>
        <li className="nav-item">
          <button 
            className={`nav-link ${activeTab === 'profile' ? 'active' : ''}`}
            onClick={() => setActiveTab('profile')}
          >
            Profile
          </button>
        </li>
      </ul>

      {activeTab === 'overview' && (
        <div>
          <div className="row mb-4">
            <div className="col-md-4 mb-3">
              <div className="card">
                <div className="card-body">
                  <h5 className="card-title">My Products</h5>
                  <p className="card-text">Manage your product listings</p>
                  <button 
                    className="btn btn-primary"
                    onClick={() => setActiveTab('products')}
                  >
                    View Products
                  </button>
                </div>
              </div>
            </div>
            
            <div className="col-md-4 mb-3">
              <div className="card">
                <div className="card-body">
                  <h5 className="card-title">Orders</h5>
                  <p className="card-text">Track customer orders</p>
                  <button 
                    className="btn btn-primary"
                    onClick={() => setActiveTab('orders')}
                  >
                    View Orders
                  </button>
                </div>
              </div>
            </div>
            
            <div className="col-md-4 mb-3">
              <div className="card">
                <div className="card-body">
                  <h5 className="card-title">Profile</h5>
                  <p className="card-text">Update business information</p>
                  <button 
                    className="btn btn-primary"
                    onClick={() => setActiveTab('profile')}
                  >
                    Edit Profile
                  </button>
                </div>
              </div>
            </div>
          </div>

          <div className="card">
            <div className="card-body">
              <h5 className="card-title">Welcome, {user?.email}!</h5>
              <p className="card-text">Role: {user?.role}</p>
              <p className="card-text">User ID: {user?.userId}</p>
            </div>
          </div>
        </div>
      )}

      {activeTab === 'products' && <ProductManagement />}

      {activeTab === 'orders' && (
        <div>
          <h4>Orders Management</h4>
          <p>Orders functionality coming soon...</p>
        </div>
      )}

      {activeTab === 'profile' && <VendorProfileManagement />}
    </div>
  );
};

export default VendorDashboard;
