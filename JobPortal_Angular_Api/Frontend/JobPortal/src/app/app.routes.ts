import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Register } from './register/register';
import { roleGuard } from './auth/role-guard';
import { authGuard } from './auth/auth-guard';
import { UserRole } from './Models/UserRole';
import { CandidateDashboard } from './candidate/candidate-dashboard/candidate-dashboard';
import { Recruiter } from './recruiter/recruiter/recruiter';
import { Admin } from './admin/admin/admin';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  {
    path: 'candidate',
    component: CandidateDashboard,
    canActivate: [authGuard, roleGuard],
    data: { roles: [UserRole.Candidate] }
  },
  {
    path: 'recruiter',
    component: Recruiter,
    canActivate: [authGuard, roleGuard],
    data: { roles: [UserRole.Recruiter] }
  },
  {
    path: 'admin',
    component: Admin,
    canActivate: [authGuard, roleGuard],
    data: { roles: [UserRole.Admin] }
  }
];
