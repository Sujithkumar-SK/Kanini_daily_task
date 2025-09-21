import { Routes } from '@angular/router';
import { Doctor } from './doctor/doctor';
import { authGuard } from './guards/auth-guard';
import { Login } from './login/login';
import { Doctorpost } from './doctorpost/doctorpost';
import { RoleGuard } from './guards/role-guard';

export const routes: Routes = [
  {path: '', redirectTo: 'Doctors',pathMatch: 'full'},
  {path:'login', component:Login},
  {path: 'Doctors', component: Doctor,canActivate:[authGuard]},
  {path:'Doctors/new',component:Doctorpost,canActivate:[authGuard,RoleGuard], data:{roles:['Admin']}},
  {path:'Doctors/edit/:id',component:Doctorpost,canActivate:[authGuard,RoleGuard], data:{roles:['Admin']}}
];
