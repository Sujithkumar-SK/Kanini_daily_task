import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { RecruiterService } from '../../service/recruiter-service';
import { JobService } from '../../service/job.service';
import { Auth } from '../../service/auth';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-recruiter',
  imports: [CommonModule, FormsModule],
  templateUrl: './recruiter.html',
  styleUrl: './recruiter.css'
})
export class Recruiter implements OnInit {
  activeTab = 'jobs';
  profile: any = null;
  jobs: any[] = [];
  applications: any[] = [];
  filteredApplications: any[] = [];
  message = '';
  
  // Filter properties
  searchTerm = '';
  statusFilter = '';
  tenthFilter: number | null = null;
  twelfthFilter: number | null = null;
  cgpaFilter: number | null = null;
  skillsFilter = '';
  
  // Loading states
  isLoading = true;
  loadingStates = {
    jobs: false,
    applications: false,
    profile: false
  };

  // Job management
  isCreatingJob = false;
  isEditingJob = false;
  selectedJob: any = null;
  newJob: any = {
    title: '',
    description: '',
    location: '',
    employmentType: 'Full-time',
    salary: null,
    requirements: ''
  };
  editJob: any = {};

  // Profile management
  isEditingProfile = false;
  editProfile: any = {};
  isUploadingImage = false;
  selectedApplication: any = null;
  defaultProfileImage = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTAwIiBoZWlnaHQ9IjEwMCIgdmlld0JveD0iMCAwIDEwMCAxMDAiIGZpbGw9Im5vbmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+CjxyZWN0IHdpZHRoPSIxMDAiIGhlaWdodD0iMTAwIiBmaWxsPSIjRjNGNEY2Ii8+CjxjaXJjbGUgY3g9IjUwIiBjeT0iMzciIHI9IjE1IiBmaWxsPSIjOUNBM0FGIi8+CjxwYXRoIGQ9Ik0yMCA4MEMyMCA2OS4wNTQzIDI4LjA1NDMgNjAgMzkgNjBINjFDNzEuOTQ1NyA2MCA4MCA2OS4wNTQzIDgwIDgwVjEwMEgyMFY4MFoiIGZpbGw9IiM5Q0EzQUYiLz4KPC9zdmc+';

  constructor(
    private recruiterService: RecruiterService,
    private jobService: JobService,
    private auth: Auth,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadJobs();
    this.loadApplications();
    this.loadProfile();
  }

  checkLoadingComplete() {
    if (this.loadingStates.jobs && this.loadingStates.applications && this.loadingStates.profile) {
      this.isLoading = false;
    }
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    this.message = '';
    this.selectedJob = null;
  }

  // Job Management
  loadJobs() {
    this.recruiterService.getMyJobs().subscribe({
      next: (data) => {
        const currentUser = this.auth.getCurrentUser();
        console.log('Current user:', currentUser);
        console.log('All jobs:', data);
        
        // Filter jobs to show only those posted by current recruiter
        // Check by email since we have that in the current user
        this.jobs = data.filter(job => {
          console.log('Job recruiter email:', job.recruiter?.email, 'Current user email:', currentUser?.email);
          return job.recruiter?.email === currentUser?.email;
        });
        
        console.log('Filtered jobs:', this.jobs);
        this.loadingStates.jobs = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching jobs', err);
        this.loadingStates.jobs = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      }
    });
  }

  startCreateJob() {
    this.isCreatingJob = true;
    this.newJob = {
      title: '',
      companyName: this.profile?.CompanyName || this.profile?.companyName || '',
      description: '',
      location: '',
      employmentType: 'Full-time',
      salary: null,
      requirements: ''
    };
  }

  cancelCreateJob() {
    this.isCreatingJob = false;
    this.newJob = {};
  }

  createJob() {
    if (!this.newJob.title || !this.newJob.companyName || !this.newJob.description || !this.newJob.location) {
      this.message = 'Please fill in all required fields';
      return;
    }

    // Prepare job data - don't include recruiter object, backend will handle the relationship
    const jobData = {
      title: this.newJob.title,
      description: this.newJob.description,
      location: this.newJob.location,
      employmentType: this.newJob.employmentType || 'Full-time',
      salary: this.newJob.salary || null,
      isActive: true
    };

    this.jobService.createJob(jobData).subscribe({
      next: () => {
        this.message = 'Job created successfully!';
        this.isCreatingJob = false;
        this.loadJobs();
      },
      error: (err) => {
        this.message = 'Failed to create job';
        console.error('Job creation failed', err);
      }
    });
  }

  editJobDetails(job: any) {
    this.isEditingJob = true;
    this.editJob = { ...job };
  }

  cancelEditJob() {
    this.isEditingJob = false;
    this.editJob = {};
  }

  updateJob() {
    this.jobService.updateJob(this.editJob.jobId, this.editJob).subscribe({
      next: () => {
        this.message = 'Job updated successfully!';
        this.isEditingJob = false;
        this.loadJobs();
      },
      error: (err) => {
        this.message = 'Failed to update job';
        console.error('Job update failed', err);
      }
    });
  }

  deleteJob(jobId: number) {
    if (confirm('Are you sure you want to delete this job?')) {
      this.jobService.deleteJob(jobId).subscribe({
        next: () => {
          this.message = 'Job deleted successfully!';
          this.loadJobs();
        },
        error: (err) => {
          this.message = 'Failed to delete job';
          console.error('Job deletion failed', err);
        }
      });
    }
  }

  viewJobApplications(job: any) {
    this.selectedJob = job;
    this.loadJobApplications(job.jobId);
  }

  loadJobApplications(jobId: number) {
    this.recruiterService.getJobApplications(jobId).subscribe({
      next: (data) => {
        this.applications = data;
        this.filteredApplications = [...data];
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching applications', err);
      }
    });
  }

  // Application Management
  loadApplications() {
    this.recruiterService.getAllApplications().subscribe({
      next: (data) => {
        console.log('Applications data:', data);
        console.log('Data type:', typeof data);
        console.log('Is array:', Array.isArray(data));
        
        this.applications = data;
        this.filteredApplications = [...data];
        
        // Log first application to check structure
        if (data.length > 0) {
          console.log('First application full object:', JSON.stringify(data[0], null, 2));
          console.log('First application keys:', Object.keys(data[0]));
          console.log('Candidate data:', data[0].candidate);
          console.log('User details:', data[0].candidate?.userDetails);
          
          // Check all possible candidate-related properties
          console.log('candidateName:', data[0].candidateName);
          console.log('candidateId:', data[0].candidateId);
          
          // Check if candidate data exists in different property names
          Object.keys(data[0]).forEach(key => {
            if (key.toLowerCase().includes('candidate') || key.toLowerCase().includes('user')) {
              console.log(`Found candidate-related property ${key}:`, data[0][key]);
            }
          });
        }
        
        // Backend now includes candidate data, no need to fetch separately
        
        this.loadingStates.applications = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching applications', err);
        this.loadingStates.applications = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      }
    });
  }



  updateApplicationStatus(applicationId: number, status: string) {
    this.recruiterService.updateApplicationStatus(applicationId, status).subscribe({
      next: () => {
        this.message = `Application ${status.toLowerCase()} successfully!`;
        if (this.selectedJob) {
          this.loadJobApplications(this.selectedJob.jobId);
        } else {
          this.loadApplications();
        }
      },
      error: (err) => {
        this.message = 'Failed to update application status';
        console.error('Status update failed', err);
      }
    });
  }

  // Profile Management
  loadProfile() {
    this.recruiterService.getProfile().subscribe({
      next: (data) => {
        console.log('Profile data:', data);
        this.profile = data;
        this.loadingStates.profile = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching profile', err);
        this.profile = {
          fullName: 'Recruiter',
          email: 'recruiter@example.com',
          role: 1,
          createdOn: new Date()
        };
        this.loadingStates.profile = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      }
    });
  }

  startEditProfile() {
    this.isEditingProfile = true;
    this.editProfile = {
      fullName: this.profile.fullName || '',
      email: this.profile.email || '',
      companyName: this.profile.CompanyName || '',
      companyWebsite: this.profile.CompanyWebsite || '',
      companyDescription: this.profile.CompanyDescription || ''
    };
  }

  cancelEditProfile() {
    this.isEditingProfile = false;
    this.editProfile = {};
  }

  saveProfile() {
    const profileData = {
      fullName: this.editProfile.fullName,
      email: this.editProfile.email,
      companyName: this.editProfile.companyName,
      companyWebsite: this.editProfile.companyWebsite,
      companyDescription: this.editProfile.companyDescription
    };
    
    this.recruiterService.updateProfile(profileData).subscribe({
      next: () => {
        this.message = 'Profile updated successfully!';
        this.isEditingProfile = false;
        this.loadProfile();
      },
      error: (err) => {
        this.message = 'Failed to update profile';
        console.error('Profile update failed', err);
      }
    });
  }

  uploadProfileImage(event: any) {
    const file = event.target.files[0];
    if (file) {
      if (!file.type.startsWith('image/')) {
        this.message = 'Please select a valid image file';
        return;
      }
      
      if (file.size > 2 * 1024 * 1024) {
        this.message = 'Image size should be less than 2MB';
        return;
      }

      this.isUploadingImage = true;
      this.message = '';
      
      const reader = new FileReader();
      reader.onload = () => {
        const base64 = reader.result as string;
        
        const profileData = {
          fullName: this.profile.fullName,
          email: this.profile.email,
          profileImage: base64
        };
        
        this.recruiterService.updateProfile(profileData).subscribe({
          next: () => {
            this.profile.profileImage = base64;
            this.message = 'Profile image updated successfully!';
            this.isUploadingImage = false;
            this.cdr.detectChanges();
            event.target.value = '';
          },
          error: (err) => {
            this.message = 'Failed to update profile image';
            this.isUploadingImage = false;
            console.error('Image upload failed', err);
          }
        });
      };
      reader.onerror = () => {
        this.message = 'Failed to read image file';
        this.isUploadingImage = false;
      };
      reader.readAsDataURL(file);
    }
  }

  getProfileImageUrl(): string {
    if (this.profile?.profileImage) {
      if (typeof this.profile.profileImage === 'string') {
        if (this.profile.profileImage.startsWith('data:')) {
          return this.profile.profileImage;
        }
        return `data:image/jpeg;base64,${this.profile.profileImage}`;
      }
    }
    return this.defaultProfileImage;
  }

  getRoleDisplayName(role: any): string {
    if (role === null || role === undefined) return 'Unknown';
    if (typeof role === 'string') return role;
    switch(role) {
      case 0: return 'Admin';
      case 1: return 'Recruiter';
      case 2: return 'Candidate';
      default: return 'Unknown';
    }
  }

  getApplicationStatus(status: string): string {
    switch(status.toLowerCase()) {
      case 'pending':
      case 'applied': return 'warning';
      case 'accepted': return 'success';
      case 'rejected': return 'danger';
      default: return 'secondary';
    }
  }

  getCandidateProfileImage(candidate: any): string {
    if (candidate?.profileImage) {
      if (typeof candidate.profileImage === 'string') {
        if (candidate.profileImage.startsWith('data:')) {
          return candidate.profileImage;
        }
        return `data:image/jpeg;base64,${candidate.profileImage}`;
      }
    }
    return this.defaultProfileImage;
  }

  getCandidateSkills(candidate: any): string[] {
    if (!candidate?.userDetails) {
      console.log('No userDetails found for candidate:', candidate);
      return [];
    }
    const skills = candidate.userDetails
      .filter((detail: any) => 
        detail.detailType === 'Skill' || 
        detail.detailType === 8 || 
        (typeof detail.detailType === 'string' && detail.detailType.toLowerCase() === 'skill')
      )
      .map((detail: any) => detail.value);
    console.log('Extracted skills:', skills);
    return skills;
  }

  getCandidateQualifications(candidate: any): any[] {
    if (!candidate?.userDetails) {
      console.log('No userDetails found for qualifications:', candidate);
      return [];
    }
    const qualifications = candidate.userDetails
      .filter((detail: any) => 
        detail.detailType !== 'Skill' && 
        detail.detailType !== 8 && 
        !(typeof detail.detailType === 'string' && detail.detailType.toLowerCase() === 'skill')
      )
      .map((detail: any) => ({
        type: this.getDetailTypeName(detail.detailType),
        value: detail.value
      }));
    console.log('Extracted qualifications:', qualifications);
    return qualifications;
  }

  getDetailTypeName(detailType: any): string {
    const typeMap: { [key: number]: string } = {
      0: 'Tenth',
      1: 'Twelfth', 
      2: 'Diploma',
      3: 'BE',
      4: 'BSc',
      5: 'BCom',
      6: 'PG',
      7: 'Certification',
      8: 'Skill'
    };
    
    if (typeof detailType === 'number') {
      return typeMap[detailType] || detailType.toString();
    }
    return detailType;
  }

  downloadResume(resume: any) {
    if (!resume || !resume.fileData) {
      this.message = 'Resume not available for download';
      return;
    }
    
    try {
      // Convert base64 string to byte array
      const byteCharacters = atob(resume.fileData);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);
      
      // Create blob and download
      const blob = new Blob([byteArray], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = resume.fileName || 'resume.pdf';
      link.click();
      window.URL.revokeObjectURL(url);
    } catch (error) {
      console.error('Error downloading resume:', error);
      this.message = 'Failed to download resume';
    }
  }

  // Helper methods to extract candidate information
  getCandidateName(app: any): string {
    return app.candidateName || app.candidate?.fullName || app.candidate?.name || 'Unknown Candidate';
  }

  getCandidateEmail(app: any): string {
    return app.candidate?.email || 'Email not available';
  }

  getCandidateRole(app: any): string {
    if (app.candidate?.role !== undefined) {
      return this.getRoleDisplayName(app.candidate.role);
    }
    return 'Candidate';
  }

  getCandidateMemberSince(app: any): string {
    const createdOn = app.candidate?.createdOn;
    if (createdOn) {
      return new Date(createdOn).toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
      });
    }
    return 'Not Available';
  }

  // Filter methods
  applyFilters() {
    this.filteredApplications = this.applications.filter(app => {
      // Search term filter
      if (this.searchTerm) {
        const searchLower = this.searchTerm.toLowerCase();
        const name = this.getCandidateName(app).toLowerCase();
        const email = this.getCandidateEmail(app).toLowerCase();
        const skills = this.getCandidateSkills(app.candidate).join(' ').toLowerCase();
        
        if (!name.includes(searchLower) && !email.includes(searchLower) && !skills.includes(searchLower)) {
          return false;
        }
      }

      // Status filter
      if (this.statusFilter && app.status !== this.statusFilter) {
        return false;
      }

      // Academic filters
      const qualifications = this.getCandidateQualifications(app.candidate);
      
      if (this.tenthFilter) {
        const tenth = qualifications.find(q => q.type === 'Tenth');
        if (!tenth || parseFloat(tenth.value.replace('%', '')) < this.tenthFilter) {
          return false;
        }
      }

      if (this.twelfthFilter) {
        const twelfth = qualifications.find(q => q.type === 'Twelfth');
        if (!twelfth || parseFloat(twelfth.value.replace('%', '')) < this.twelfthFilter) {
          return false;
        }
      }

      if (this.cgpaFilter) {
        const cgpa = qualifications.find(q => q.type === 'BE' || q.type === 'BSc' || q.type === 'BCom');
        if (!cgpa || parseFloat(cgpa.value) < this.cgpaFilter) {
          return false;
        }
      }

      // Skills filter
      if (this.skillsFilter) {
        const requiredSkills = this.skillsFilter.toLowerCase().split(',').map(s => s.trim());
        const candidateSkills = this.getCandidateSkills(app.candidate).map(s => s.toLowerCase());
        
        const hasAnySkill = requiredSkills.some(skill => 
          candidateSkills.some(candidateSkill => candidateSkill.includes(skill))
        );
        
        if (!hasAnySkill) {
          return false;
        }
      }

      return true;
    });
  }

  clearFilters() {
    this.searchTerm = '';
    this.statusFilter = '';
    this.tenthFilter = null;
    this.twelfthFilter = null;
    this.cgpaFilter = null;
    this.skillsFilter = '';
    this.filteredApplications = [...this.applications];
  }

  toggleFullProfile(app: any) {
    if (this.selectedApplication?.applicationId === app.applicationId) {
      this.selectedApplication = null;
    } else {
      this.selectedApplication = app;
    }
  }

}