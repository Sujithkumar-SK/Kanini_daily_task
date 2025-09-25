import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Auth } from './auth';

@Injectable({
  providedIn: 'root'
})
export class RecruiterService {
  private candidateUrl = "http://localhost:5075/api/Candidate";
  private jobUrl = "http://localhost:5075/api/Job";
  private applicationUrl = "http://localhost:5075/api/Application";

  constructor(private http: HttpClient, private auth: Auth) { }

  // Profile - Using Candidate endpoints as there's no separate Recruiter profile endpoint
  getProfile(): Observable<any> {
    return this.http.get(`${this.candidateUrl}/me`);
  }

  updateProfile(data: any): Observable<any> {
    return this.http.put(`${this.candidateUrl}/me`, data);
  }

  // Jobs - Get all jobs and filter by recruiter on frontend
  getMyJobs(): Observable<any[]> {
    return this.http.get<any[]>(this.jobUrl);
  }

  // Applications
  getAllApplications(): Observable<any[]> {
    // Get all applications and filter by recruiter's jobs on frontend
    return this.http.get<any[]>(`${this.applicationUrl}/recruiter`);
  }

  getJobApplications(jobId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.applicationUrl}/job/${jobId}`);
  }

  updateApplicationStatus(applicationId: number, status: string): Observable<any> {
    return this.http.put(`${this.applicationUrl}/${applicationId}`, { status, isActive: true });
  }

  // Get candidate details by ID
  getCandidateById(candidateId: number): Observable<any> {
    return this.http.get(`${this.candidateUrl}/${candidateId}`);
  }


}
