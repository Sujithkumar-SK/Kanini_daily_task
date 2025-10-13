import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-component',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css'
})
export class RegisterComponent {
  registerForm!: FormGroup;
  ngOnInit(){
    this.registerForm = this.fb.group({
      fullName:['',[Validators.required,Validators.minLength(3)]],
      email:['',[Validators.required,Validators.email]],
      password:['',[Validators.required,Validators.minLength(6)]]
    });
  }
  
  constructor(private fb: FormBuilder, private router: Router){}
  onSubmit(){
    if(this.registerForm.invalid){
      this.registerForm.markAllAsTouched();
      return;
    }
    const users = JSON.parse(localStorage.getItem('users')||'[]');
    users.push(this.registerForm.value);
    localStorage.setItem('users',JSON.stringify(users));
    alert('Registration Successful');
    this.router.navigate(['/login']);
  }
}
