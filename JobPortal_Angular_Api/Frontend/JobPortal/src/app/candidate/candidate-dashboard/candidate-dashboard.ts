import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CandidateService } from '../../service/candidate-service';
import { JobService } from '../../service/job.service';
import { Auth } from '../../service/auth';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Job } from '../../Models/Job.model';
import { Application } from '../../Models/Application.model';

@Component({
  selector: 'app-candidate-dashboard',
  imports: [CommonModule, FormsModule],
  templateUrl: './candidate-dashboard.html',
  styleUrl: './candidate-dashboard.css'
})
export class CandidateDashboard implements OnInit {
  activeTab = 'jobs';
  resumes: any[] = [];
  applications: Application[] = [];
  jobs: Job[] = [];
  profile: any = {};
  selectedJob: Job | null = null;
  selectedResumeId: number | null = null;
  selectedApplication: any = null;
  message = '';
  searchTerm = '';
  filteredJobs: Job[] = [];
  isUploading = false;
  customResumeName = '';
  isLoading = true;
  loadingStates = {
    jobs: false,
    resumes: false,
    applications: false,
    profile: false
  };
  isEditingProfile = false;
  editProfile: any = {};
  newSkill = '';
  newEducation = '';
  newDetailType = '';
  newDetailValue = '';
  detailTypes = [
    'Tenth', 'Twelfth', 'Diploma', 'BE', 'BSc', 'BCom', 'PG', 'Certification'
  ];
  isUploadingImage = false;
  defaultProfileImage = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTAwIiBoZWlnaHQ9IjEwMCIgdmlld0JveD0iMCAwIDEwMCAxMDAiIGZpbGw9Im5vbmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+CjxyZWN0IHdpZHRoPSIxMDAiIGhlaWdodD0iMTAwIiBmaWxsPSIjRjNGNEY2Ii8+CjxjaXJjbGUgY3g9IjUwIiBjeT0iMzciIHI9IjE1IiBmaWxsPSIjOUNBM0FGIi8+CjxwYXRoIGQ9Ik0yMCA4MEMyMCA2OS4wNTQzIDI4LjA1NDMgNjAgMzkgNjBINjFDNzEuOTQ1NyA2MCA4MCA2OS4wNTQzIDgwIDgwVjEwMEgyMFY4MFoiIGZpbGw9IiM5Q0EzQUYiLz4KPC9zdmc+';

  constructor(
    private candidateService: CandidateService,
    private jobService: JobService,
    private auth: Auth,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadJobs();
    this.loadResumes();
    this.loadApplications();
    this.loadProfile();
  }

  checkLoadingComplete() {
    if (this.loadingStates.jobs && this.loadingStates.resumes && 
        this.loadingStates.applications && this.loadingStates.profile) {
      this.isLoading = false;
    }
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    this.message = '';
  }

  loadJobs() {
    this.jobService.getAllJobs().subscribe({
      next: (data) => {
        this.jobs = data.filter(job => job.isActive);
        this.filteredJobs = this.jobs;
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

  searchJobs() {
    if (!this.searchTerm.trim()) {
      this.filteredJobs = this.jobs;
      return;
    }
    this.filteredJobs = this.jobs.filter(job => 
      job.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      job.location.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      job.description.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  viewJobDetails(job: Job) {
    this.selectedJob = job;
  }

  closeJobDetails() {
    this.selectedJob = null;
    this.selectedResumeId = null;
  }

  applyToJob() {
    if (!this.selectedJob || !this.selectedResumeId) {
      this.message = 'Please select a resume to apply';
      return;
    }

    // Check if already applied
    const alreadyApplied = this.applications.some(app => 
      app.jobId === this.selectedJob!.jobId
    );

    if (alreadyApplied) {
      this.message = 'You have already applied to this job';
      return;
    }

    this.jobService.applyToJob(this.selectedJob.jobId, this.selectedResumeId).subscribe({
      next: () => {
        this.message = 'Application submitted successfully!';
        this.loadApplications();
        // Auto close modal after 1.5 seconds
        setTimeout(() => {
          this.closeJobDetails();
          this.message = '';
        }, 1500);
      },
      error: (err) => {
        this.message = 'Failed to submit application';
        console.error('Application failed', err);
      }
    });
  }

  loadResumes() {
    this.candidateService.getResumes().subscribe({
      next: (data) => {
        this.resumes = data;
        this.loadingStates.resumes = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching resumes', err);
        this.loadingStates.resumes = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      }
    });
  }

  uploadResume(event: any) {
    const file = event.target.files[0];
    if (file) {
      if (!this.customResumeName.trim()) {
        this.message = 'Please enter a resume name';
        return;
      }
      this.isUploading = true;
      this.message = '';
      this.candidateService.uploadResume(file, this.customResumeName.trim()).subscribe({
        next: () => {
          this.loadResumes();
          this.message = 'Resume uploaded successfully!';
          this.isUploading = false;
          this.customResumeName = '';
          // Reset file input
          event.target.value = '';
        },
        error: (err) => {
          this.message = 'Failed to upload resume';
          this.isUploading = false;
          console.error('Upload failed', err);
        }
      });
    }
  }

  deleteResume(id: number) {
    if (confirm('Are you sure you want to delete this resume?')) {
      this.candidateService.deleteResume(id).subscribe({
        next: () => {
          this.loadResumes();
          this.message = 'Resume deleted successfully!';
        },
        error: (err) => {
          this.message = 'Failed to delete resume';
          console.error('Delete failed', err);
        }
      });
    }
  }

  loadApplications() {
    this.candidateService.getApplications().subscribe({
      next: (data) => {
        console.log('Applications data:', data);
        this.applications = data;
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

  loadProfile() {
    this.candidateService.getProfile().subscribe({
      next: (data) => {
        this.profile = data;
        this.loadingStates.profile = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error fetching profile', err);
        // Set a default profile if API fails
        this.profile = {
          fullName: 'User',
          email: 'user@example.com',
          role: 2,
          createdOn: new Date(),
          userDetails: []
        };
        this.loadingStates.profile = true;
        this.checkLoadingComplete();
        this.cdr.detectChanges();
      }
    });
  }

  withdrawApplication(applicationId: number) {
    if (confirm('Are you sure you want to withdraw this application?')) {
      this.candidateService.withdrawApplication(applicationId).subscribe({
        next: () => {
          this.message = 'Application withdrawn successfully!';
          // Remove the application from the local array immediately
          this.applications = this.applications.filter(app => app.applicationId !== applicationId);
          // Force change detection
          this.cdr.detectChanges();
          // Also reload from server to ensure consistency
          this.loadApplications();
        },
        error: (err) => {
          this.message = 'Failed to withdraw application';
          console.error('Withdraw failed', err);
        }
      });
    }
  }

  startEditProfile() {
    this.isEditingProfile = true;
    this.editProfile = {
      fullName: this.profile.fullName || '',
      email: this.profile.email || '',
      skills: this.getSkillsFromUserDetails(),
      qualifications: this.getQualificationsFromUserDetails()
    };
  }

  getSkillsFromUserDetails(): string[] {
    if (!this.profile.userDetails) return [];
    return this.profile.userDetails
      .filter((detail: any) => 
        detail.detailType === 'Skill' || 
        detail.detailType === 8 || // DetailType.Skill enum value
        (typeof detail.detailType === 'string' && detail.detailType.toLowerCase() === 'skill')
      )
      .map((detail: any) => detail.value);
  }

  getQualificationsFromUserDetails(): any[] {
    if (!this.profile.userDetails) return [];
    return this.profile.userDetails
      .filter((detail: any) => 
        detail.detailType !== 'Skill' && 
        detail.detailType !== 8 && // DetailType.Skill enum value
        !(typeof detail.detailType === 'string' && detail.detailType.toLowerCase() === 'skill')
      )
      .map((detail: any) => ({
        type: this.getDetailTypeName(detail.detailType),
        value: detail.value
      }));
  }

  getDetailTypeName(detailType: any): string {
    // Convert enum number to name
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

  cancelEditProfile() {
    this.isEditingProfile = false;
    this.editProfile = {};
    this.newSkill = '';
    this.newDetailType = '';
    this.newDetailValue = '';
  }

  saveProfile() {
    // Convert qualifications to the format backend expects
    const profileData = {
      fullName: this.editProfile.fullName,
      email: this.editProfile.email,
      skills: this.editProfile.skills,
      qualifications: this.editProfile.qualifications?.map((qual: any) => 
        `${qual.type}:${qual.value}`
      )
    };

    this.candidateService.updateProfile(profileData).subscribe({
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

  addSkill() {
    if (this.newSkill.trim()) {
      if (!this.editProfile.skills) this.editProfile.skills = [];
      this.editProfile.skills.push(this.newSkill.trim());
      this.newSkill = '';
    }
  }

  removeSkill(index: number) {
    this.editProfile.skills.splice(index, 1);
  }

  addEducation() {
    if (this.newEducation.trim()) {
      if (!this.editProfile.education) this.editProfile.education = [];
      this.editProfile.education.push(this.newEducation.trim());
      this.newEducation = '';
    }
  }

  addQualification() {
    if (this.newDetailType && this.newDetailValue.trim()) {
      if (!this.editProfile.qualifications) this.editProfile.qualifications = [];
      this.editProfile.qualifications.push({
        type: this.newDetailType,
        value: this.newDetailValue.trim()
      });
      this.newDetailType = '';
      this.newDetailValue = '';
    }
  }

  removeQualification(index: number) {
    this.editProfile.qualifications.splice(index, 1);
  }

  uploadProfileImage(event: any) {
    const file = event.target.files[0];
    if (file) {
      // Validate file type
      if (!file.type.startsWith('image/')) {
        this.message = 'Please select a valid image file';
        return;
      }
      
      // Validate file size (max 2MB)
      if (file.size > 2 * 1024 * 1024) {
        this.message = 'Image size should be less than 2MB';
        return;
      }

      this.isUploadingImage = true;
      this.message = '';
      
      const reader = new FileReader();
      reader.onload = () => {
        const base64 = reader.result as string;
        
        // Update profile with new image
        const profileData = {
          fullName: this.profile.fullName,
          email: this.profile.email,
          profileImage: base64
        };
        
        this.candidateService.updateProfile(profileData).subscribe({
          next: () => {
            this.profile.profileImage = base64;
            this.message = 'Profile image updated successfully!';
            this.isUploadingImage = false;
            this.cdr.detectChanges();
            // Reset file input
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
      // If profileImage is a byte array (comes as base64 string from backend)
      if (typeof this.profile.profileImage === 'string') {
        // Check if it's already a data URL
        if (this.profile.profileImage.startsWith('data:')) {
          return this.profile.profileImage;
        }
        // Convert base64 string to data URL
        return `data:image/jpeg;base64,${this.profile.profileImage}`;
      }
    }
    return this.defaultProfileImage;
  }

  getRoleDisplayName(role: any): string {
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
      case 'pending': return 'app-status-warning';
      case 'accepted': return 'app-status-success';
      case 'rejected': return 'app-status-danger';
      default: return 'app-status-secondary';
    }
  }

  viewResume(resume: any) {
    if (!resume || !resume.resumeId) {
      this.message = 'Resume not available for viewing';
      return;
    }
    
    this.candidateService.getResumeById(resume.resumeId).subscribe({
      next: (fullResume) => {
        try {
          const fileData = fullResume.fileData;
          if (!fileData) {
            this.message = 'Resume file data not available';
            return;
          }
          
          const byteCharacters = atob(fileData);
          const byteNumbers = new Array(byteCharacters.length);
          for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
          }
          const byteArray = new Uint8Array(byteNumbers);
          
          const blob = new Blob([byteArray], { type: 'application/pdf' });
          const url = window.URL.createObjectURL(blob);
          window.open(url, '_blank');
          
          setTimeout(() => {
            window.URL.revokeObjectURL(url);
          }, 1000);
        } catch (error) {
          console.error('Error viewing resume:', error);
          this.message = 'Failed to view resume';
        }
      },
      error: (err) => {
        console.error('Error fetching resume:', err);
        this.message = 'Failed to fetch resume data';
      }
    });
  }

  downloadResume(resume: any) {
    if (!resume || !resume.resumeId) {
      this.message = 'Resume not available for download';
      return;
    }
    
    this.candidateService.getResumeById(resume.resumeId).subscribe({
      next: (fullResume) => {
        try {
          const fileData = fullResume.fileData;
          if (!fileData) {
            this.message = 'Resume file data not available';
            return;
          }
          
          const byteCharacters = atob(fileData);
          const byteNumbers = new Array(byteCharacters.length);
          for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
          }
          const byteArray = new Uint8Array(byteNumbers);
          
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
      },
      error: (err) => {
        console.error('Error fetching resume:', err);
        this.message = 'Failed to fetch resume data';
      }
    });
  }
  
  // Application Status Tracking Methods
  getStepStatus(status: string, step: string): boolean {
    const currentStatus = status?.toLowerCase() || 'pending';
    
    switch (step) {
      case 'applied':
        return true; // Always completed since application exists
      case 'review':
        return ['accepted', 'rejected'].includes(currentStatus);
      case 'decision':
        return ['accepted', 'rejected'].includes(currentStatus);
      default:
        return false;
    }
  }
  
  getCurrentStep(status: string): string {
    const currentStatus = status?.toLowerCase() || 'pending';
    
    switch (currentStatus) {
      case 'pending':
      case 'applied':
        return 'applied';
      case 'under review':
      case 'reviewing':
        return 'review';
      case 'accepted':
      case 'rejected':
        return 'decision';
      default:
        return 'review'; // Default to review for unknown statuses
    }
  }
  
  getProgressPercentage(status: string): number {
    const currentStatus = status?.toLowerCase() || 'pending';
    
    switch (currentStatus) {
      case 'pending':
      case 'applied':
        return 33;
      case 'under review':
      case 'reviewing':
        return 66;
      case 'accepted':
      case 'rejected':
        return 100;
      default:
        return 66;
    }
  }
  
  getStatusDisplayText(status: string): string {
    if (!status) return 'Pending';
    
    const currentStatus = status.toLowerCase();
    
    switch (currentStatus) {
      case 'pending':
        return 'Pending';
      case 'applied':
        return 'Applied';
      case 'under review':
        return 'Under Review';
      case 'reviewing':
        return 'Under Review';
      case 'accepted':
        return 'Accepted 🎉';
      case 'rejected':
        return 'Rejected';
      default:
        return status;
    }
  }
  
  viewApplicationDetails(application: any) {
    this.selectedApplication = application;
  }
  
  closeApplicationDetails() {
    this.selectedApplication = null;
  }
  
  canWithdrawApplication(status: string): boolean {
    if (!status) return true; // Allow withdrawal if no status (pending)
    
    const currentStatus = status.toLowerCase();
    // Only allow withdrawal for pending, applied, or under review applications
    return ['pending', 'applied', 'under review', 'reviewing'].includes(currentStatus);
  }

}
