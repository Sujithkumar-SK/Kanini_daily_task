import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  private candidateUrl = "http://localhost:5075/api/Candidate";
  private resumeUrl = "http://localhost:5075/api/Resume";
  private applicationUrl = "http://localhost:5075/api/Application";

  constructor(private http: HttpClient) { }

  // Resumes
  getResumes(): Observable<any[]> {
    return this.http.get<any[]>(this.resumeUrl);
  }
  uploadResume(file: File, customName?: string): Observable<any> {
    return new Observable(observer => {
      const reader = new FileReader();
      reader.onload = () => {
        const base64 = reader.result as string;
        const fileData = base64.split(',')[1]; // Remove data:type;base64, prefix
        
        const resumeDto = {
          fileName: customName || file.name,
          fileData: fileData,
          fileSize: file.size
        };
        
        this.http.post(this.resumeUrl, resumeDto).subscribe({
          next: (result) => {
            observer.next(result);
            observer.complete();
          },
          error: (error) => observer.error(error)
        });
      };
      reader.onerror = () => observer.error('Failed to read file');
      reader.readAsDataURL(file);
    });
  }
  deleteResume(id: number): Observable<any> {
    return this.http.delete(`${this.resumeUrl}/${id}`);
  }

  getResumeById(id: number): Observable<any> {
    return this.http.get(`${this.candidateUrl}/resume/${id}`);
  }

  // Applied Jobs
  getApplications(): Observable<any[]> {
    return this.http.get<any[]>(`${this.applicationUrl}/candidate`);
  }

  withdrawApplication(applicationId: number): Observable<any> {
    return this.http.delete(`${this.applicationUrl}/${applicationId}`);
  }

  // Profile
  getProfile(): Observable<any> {
    return this.http.get(`${this.candidateUrl}/me`);
  }
  updateProfile(data: any): Observable<any> {
    return this.http.put(`${this.candidateUrl}/me`, data);
  }
}
