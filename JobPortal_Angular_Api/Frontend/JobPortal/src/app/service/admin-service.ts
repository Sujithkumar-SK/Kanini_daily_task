import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private adminUrl = 'http://localhost:5075/api/Admin';

  constructor(private http: HttpClient) { }

  // Analytics
  getAnalytics(fromDate?: Date, toDate?: Date): Observable<any> {
    let params = '';
    if (fromDate && toDate) {
      params = `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}`;
    }
    return this.http.get(`${this.adminUrl}/analytics${params}`);
  }

  // User Management
  getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.adminUrl}/users`);
  }

  deactivateUser(userId: number): Observable<any> {
    return this.http.put(`${this.adminUrl}/users/${userId}/deactivate`, {});
  }

  activateUser(userId: number): Observable<any> {
    return this.http.put(`${this.adminUrl}/users/${userId}/activate`, {});
  }

  // Recruiter Management
  getAllRecruiters(): Observable<any[]> {
    return this.http.get<any[]>(`${this.adminUrl}/recruiters`);
  }

  deactivateRecruiter(recruiterId: number): Observable<any> {
    return this.http.put(`${this.adminUrl}/recruiters/${recruiterId}/deactivate`, {});
  }

  activateRecruiter(recruiterId: number): Observable<any> {
    return this.http.put(`${this.adminUrl}/recruiters/${recruiterId}/activate`, {});
  }
}
