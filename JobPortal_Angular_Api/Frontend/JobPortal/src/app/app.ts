import { Component, signal } from '@angular/core';
import { RouterOutlet, Router, RouterLink } from '@angular/router';
import { Auth } from './service/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor(public auth: Auth, private router: Router) {}
  
  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
  
  protected readonly title = signal('JobPortal');
}
