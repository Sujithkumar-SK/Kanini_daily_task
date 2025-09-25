import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Auth } from '../service/auth';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule,CommonModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  form: FormGroup;
  error = '';
  constructor(private fb: FormBuilder, private auth:
    Auth, private router: Router) {
    this.form = this.fb.group({
      username: ['', Validators.required], password:
        ['', Validators.required]
    });

  }
  submit() {
    if (this.form.invalid) return;
    const { username, password } = this.form.value;
    this.auth.login(username, password).subscribe({
      next: (response) => {
        // Navigate based on user role
        switch(response.role) {
          case 'Candidate':
            this.router.navigate(['/candidate']);
            break;
          case 'Recruiter':
            this.router.navigate(['/recruiter']);
            break;
          case 'Admin':
            this.router.navigate(['/admin']);
            break;
          default:
            this.router.navigate(['/']);
        }
      },
      error: (err) => (this.error =
        err?.error?.message || 'Login failed')
    });
  }
}
