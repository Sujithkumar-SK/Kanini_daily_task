import { Component, signal } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthServices } from './auth.services';
import { Doctor } from './doctor/doctor';
import { Login } from './login/login';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Doctor, Login, CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  title = 'day6ang';
  constructor(public auth: AuthServices) { }
  logout() {
    this.auth.logout();
  }
}
