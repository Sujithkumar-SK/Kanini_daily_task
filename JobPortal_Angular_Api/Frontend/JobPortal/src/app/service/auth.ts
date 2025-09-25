import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { User } from '../Models/User.model';
import { UserRole } from '../Models/UserRole';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private base = "http://localhost:5075/api/Auth";
  constructor(private http: HttpClient) { }
  login(email: string, password: string):
    Observable<{
      token: string; name: string; email: string; role: UserRole;
    }> {
    return this.http
      .post<{
        token: string; name: string; email: string; role: UserRole
      }>(`${this.base}/login`, {
        email,
        password
      })
      .pipe(
        tap((resp) => {
          if (resp?.token) {
            localStorage.setItem('jwt_token',
              resp.token);
            localStorage.setItem('current_user',
              JSON.stringify({
                name: resp.name,
                email: resp.email,
                role: resp.role
              }));
          }
        })
      );
  }
  register(user: User): Observable<User> {
    return this.http.post<User>(`${this.base}/register`, user);
  }
  logout(): void {
    localStorage.removeItem('jwt_token');
    localStorage.removeItem('current_user');
  }
  getToken(): string | null {
    return localStorage.getItem('jwt_token');
  }
  getCurrentUser(): User | null {
    const v = localStorage.getItem('current_user');
    return v ? JSON.parse(v) : null;
  }
  isLoggedIn(): boolean {
    return !!this.getToken();
  }
  getCurrentUserRole(): UserRole | null {
    const user = this.getCurrentUser();
    return user?.role ?? null;
  }
  isAdmin(): boolean {
    return this.getCurrentUserRole() === UserRole.Admin;
  }
  isCandidate(): boolean {
    return this.getCurrentUserRole() === UserRole.Candidate;
  }
  isRecruiter(): boolean {
    return this.getCurrentUserRole() === UserRole.Recruiter;
  }
}
