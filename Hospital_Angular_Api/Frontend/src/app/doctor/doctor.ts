import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Doctors } from '../Models/Doctor.model';
import { DoctorService } from '../doctor-service';
import { Router, RouterModule } from '@angular/router';
import { AsyncPipe, CommonModule } from '@angular/common';
import { AuthServices } from '../auth.services';

@Component({
  selector: 'app-doctor',
  imports: [RouterModule, AsyncPipe,CommonModule],
  templateUrl: './doctor.html',
  styleUrl: './doctor.css'
})
export class Doctor implements OnInit{
  doctors: Doctors[] = [];
  loading = false;
  error = '';
  constructor(private service:
    DoctorService, public auth: AuthServices, private router: Router,private cdr:ChangeDetectorRef) { }
  ngOnInit() {
    this.load();
  }
  load() {
    this.loading = true;
    this.cdr.detectChanges();
    this.service.getAll().subscribe({
      next: (d) => ((this.doctors = d),
        (this.loading = false),this.cdr.detectChanges()), error: () =>
        ((this.error = 'Could not load doctors'),
        (this.loading = false),
      this.cdr.detectChanges())
    });
  }
  add() {
    console.log("welcome");
    this.router.navigate(['/Doctors/new']);
  }
  edit(id: string) {
    this.router.navigate(['/Doctors/edit', id]);
  }
  delete(id: string) {
    if (!confirm('Delete doctor?')) return;
    this.service.delete(id).subscribe({
      next: () => this.load(), error: () =>

        alert('Delete failed')
    });
  }
}
