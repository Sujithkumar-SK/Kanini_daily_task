import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Auth } from './auth';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private baseUrl = "http://localhost:5075/api/Job";
  private applicationUrl = "http://localhost:5075/api/Application";

  constructor(private http: HttpClient, private auth: Auth) { }

  getAllJobs(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  getJobById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  createJob(job: any): Observable<any> {
    // Send complete job data including recruiter info
    return this.http.post(this.baseUrl, job);
  }

  updateJob(jobId: number, job: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${jobId}`, job);
  }

  deleteJob(jobId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${jobId}`);
  }

  applyToJob(jobId: number, resumeId: number): Observable<any> {
    return this.http.post(`${this.applicationUrl}/apply`, {
      JobId: jobId,
      ResumeId: resumeId
    });
  }
}