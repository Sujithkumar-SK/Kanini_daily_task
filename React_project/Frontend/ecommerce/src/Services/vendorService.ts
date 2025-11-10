import axios from 'axios';
import type { VendorProfile, VendorProfileUpdate } from '../types/vendor';

export const vendorService = {
  getMyProfile: async (): Promise<VendorProfile> => {
    const response = await axios.get('/vendor/my-profile');
    return response.data;
  },

  updateMyProfile: async (data: VendorProfileUpdate): Promise<VendorProfile> => {
    const response = await axios.put('/vendor/my-profile', data);
    return response.data;
  },

  uploadDocument: async (vendorId: number, file: File): Promise<{ message: string; path: string }> => {
    const formData = new FormData();
    formData.append('document', file);
    const response = await axios.post(`/vendor/${vendorId}/upload-document`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' }
    });
    return response.data;
  }
};