import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Doctors } from './Models/Doctor.model';
import { Observable } from 'rxjs';
import { Doctor } from './doctor/doctor';
import { Hospital } from './Models/Hospital.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private base = "http://localhost:5245/api";
  constructor(private http: HttpClient) { }
  getAll(): Observable<Doctors[]> {
    return this.http.get<Doctors[]>(`${this.base}/Doctors`);
  }
  getById(id: string): Observable<Doctors> {
    return this.http.get<Doctors>(`${this.base}/Doctors/${id}`);
  }

  getHospitals(): Observable<Hospital[]> {
    return this.http.get<Hospital[]>(`${this.base}/Hospitals`);
  }
  add(doc: Partial<Doctors>): Observable<Doctors> {
    return this.http.post<Doctors>(`${this.base}/Doctors`, doc);
  }
  update(doc: Doctors): Observable<Doctors> {
    return this.http.put<Doctors>(`${this.base}/Doctors/${doc.doctorId}`,
      doc);
  }
  delete(id: string): Observable<any> {
    return this.http.delete(`${this.base}/Doctors/${id}`);
  }
}
