import { UserRole } from "./UserRole";

export interface User {
  userId?: number;
  fullName?: string;
  name?: string;
  email: string;
  password?: string;
  profileImage?: string;
  role: UserRole;
  isActive?: boolean;
}