import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { UserRole } from '../Models/UserRole';
import { Router } from '@angular/router';
import { Auth } from '../service/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  form: FormGroup;
  error = '';
  roles = [UserRole.Recruiter, UserRole.Candidate];
  showCandidateFields = false;
  showRecruiterFields = false;
  skills: string[] = [];
  qualifications: any[] = [];
  newSkill = '';
  newQualType = '';
  newQualValue = '';
  selectedFile: File | null = null;
  detailTypes = ['Tenth', 'Twelfth', 'Diploma', 'BE', 'BSc', 'BCom', 'PG', 'Certification'];

  constructor(private fb: FormBuilder, private auth: Auth, private router: Router) {
    this.form = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
      dateOfBirth: [''],
      role: [UserRole.Candidate, Validators.required],
      // Candidate fields
      profileImage: [''],
      // Recruiter fields
      companyName: [''],
      companyWebsite: [''],
      companyDescription: ['']
    }, { validators: this.passwordMatchValidator });
    
    this.form.get('role')?.valueChanges.subscribe(role => {
      this.showCandidateFields = role === UserRole.Candidate;
      this.showRecruiterFields = role === UserRole.Recruiter;
      this.updateValidators();
    });
    
    // Set initial state
    this.showCandidateFields = true;
  }
  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password');
    const confirmPassword = form.get('confirmPassword');
    
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    } else {
      if (confirmPassword?.hasError('passwordMismatch')) {
        confirmPassword.setErrors(null);
      }
      return null;
    }
  }

  updateValidators() {
    const companyName = this.form.get('companyName');
    if (this.showRecruiterFields) {
      companyName?.setValidators([Validators.required]);
    } else {
      companyName?.clearValidators();
    }
    companyName?.updateValueAndValidity();
  }

  submit() {
    if (this.form.invalid) return;
    
    const formData: any = {
      fullName: this.form.value.fullName,
      email: this.form.value.email,
      password: this.form.value.password,
      role: this.form.value.role,
      dateOfBirth: this.form.value.dateOfBirth || null,
      profileImage: this.form.value.profileImage || null
    };
    
    // Add candidate-specific data
    if (this.showCandidateFields) {
      formData.skills = this.skills;
      formData.qualifications = this.qualifications.map(q => `${q.type}:${q.value}`);
      if (this.selectedFile) {
        // Convert file to base64 for resume
        const reader = new FileReader();
        reader.onload = () => {
          const base64 = reader.result as string;
          formData.resume = {
            fileName: this.selectedFile!.name,
            fileData: base64.split(',')[1], // Remove data:type;base64, prefix
            fileSize: this.selectedFile!.size
          };
          this.sendRegistration(formData);
        };
        reader.readAsDataURL(this.selectedFile);
        return;
      }
    }
    
    // Add recruiter-specific data
    if (this.showRecruiterFields) {
      formData.companyName = this.form.value.companyName;
      formData.companyWebsite = this.form.value.companyWebsite;
      formData.companyDescription = this.form.value.companyDescription;
    }
    
    this.sendRegistration(formData);
  }
  
  private sendRegistration(formData: any) {
    this.auth.register(formData).subscribe({
      next: () => this.router.navigate(['/login']),
      error: (err) => this.error = err?.error?.message || 'Registration failed'
    });
  }
  
  addSkill() {
    if (this.newSkill.trim()) {
      this.skills.push(this.newSkill.trim());
      this.newSkill = '';
    }
  }
  
  removeSkill(index: number) {
    this.skills.splice(index, 1);
  }
  
  addQualification() {
    if (this.newQualType && this.newQualValue.trim()) {
      this.qualifications.push({ type: this.newQualType, value: this.newQualValue.trim() });
      this.newQualType = '';
      this.newQualValue = '';
    }
  }
  
  removeQualification(index: number) {
    this.qualifications.splice(index, 1);
  }
  
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
  
  onImageSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.form.patchValue({ profileImage: reader.result });
      };
      reader.readAsDataURL(file);
    }
  }
}
