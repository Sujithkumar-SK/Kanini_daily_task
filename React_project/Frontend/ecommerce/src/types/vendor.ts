export interface VendorProfile {
  vendorId: number;
  businessName: string;
  ownerName: string;
  businessLicenseNumber: string;
  businessAddress: string;
  city?: string;
  state?: string;
  pinCode?: string;
  taxRegistrationNumber?: string;
  documentPath?: string;
  status: string;
  currentPlan: string;
}

export interface VendorProfileUpdate {
  businessName: string;
  ownerName: string;
  businessLicenseNumber: string;
  businessAddress: string;
  city?: string;
  state?: string;
  pinCode?: string;
  taxRegistrationNumber?: string;
}