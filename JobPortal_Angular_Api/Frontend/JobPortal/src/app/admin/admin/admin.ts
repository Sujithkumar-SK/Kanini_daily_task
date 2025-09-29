import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../service/admin-service';

@Component({
  selector: 'app-admin',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.html',
  styleUrl: './admin.css'
})
export class Admin implements OnInit {
  activeTab = 'dashboard';
  
  // Analytics Data
  analytics: any = {};
  
  // User Management
  users: any[] = [];
  recruiters: any[] = [];
  
  // Loading States
  isLoading = true;
  message = '';
  
  // Filters
  userFilter = '';
  recruiterFilter = '';
  
  constructor(private adminService: AdminService, private cdr: ChangeDetectorRef) { }
  
  ngOnInit(): void {
    this.loadDashboardData();
  }
  
  setActiveTab(tab: string) {
    this.activeTab = tab;
    this.message = '';
    
    if (tab === 'users' && this.users.length === 0) {
      this.loadUsers();
    } else if (tab === 'recruiters' && this.recruiters.length === 0) {
      this.loadRecruiters();
    }
  }
  
  loadDashboardData() {
    this.isLoading = true;
    // Load analytics and users data for dashboard
    this.adminService.getAnalytics().subscribe({
      next: (data) => {
        this.analytics = data;
        // Also load users for active/inactive counts
        this.loadUsers();
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading analytics:', err);
        this.isLoading = false;
        this.message = 'Failed to load analytics data';
      }
    });
  }
  
  loadUsers() {
    this.adminService.getAllUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading users:', err);
        this.message = 'Failed to load users';
      }
    });
  }
  
  loadRecruiters() {
    this.adminService.getAllRecruiters().subscribe({
      next: (data) => {
        this.recruiters = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading recruiters:', err);
        this.message = 'Failed to load recruiters';
      }
    });
  }
  
  toggleUserStatus(user: any) {
    const action = user.isActive ? 'deactivate' : 'activate';
    const service = user.isActive ? 
      this.adminService.deactivateUser(user.userId) : 
      this.adminService.activateUser(user.userId);
    
    service.subscribe({
      next: () => {
        user.isActive = !user.isActive;
        this.message = `User ${action}d successfully`;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error(`Error ${action}ing user:`, err);
        this.message = `Failed to ${action} user`;
      }
    });
  }
  
  toggleRecruiterStatus(recruiter: any) {
    const action = recruiter.isActive ? 'deactivate' : 'activate';
    const service = recruiter.isActive ? 
      this.adminService.deactivateRecruiter(recruiter.recruiterId) : 
      this.adminService.activateRecruiter(recruiter.recruiterId);
    
    service.subscribe({
      next: () => {
        recruiter.isActive = !recruiter.isActive;
        this.message = `Recruiter ${action}d successfully`;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error(`Error ${action}ing recruiter:`, err);
        this.message = `Failed to ${action} recruiter`;
      }
    });
  }
  
  getFilteredUsers() {
    if (!this.userFilter.trim()) return this.users;
    return this.users.filter(user => 
      user.fullName.toLowerCase().includes(this.userFilter.toLowerCase()) ||
      user.email.toLowerCase().includes(this.userFilter.toLowerCase()) ||
      user.role.toLowerCase().includes(this.userFilter.toLowerCase())
    );
  }
  
  getFilteredRecruiters() {
    if (!this.recruiterFilter.trim()) return this.recruiters;
    return this.recruiters.filter(recruiter => 
      recruiter.companyName.toLowerCase().includes(this.recruiterFilter.toLowerCase()) ||
      recruiter.website.toLowerCase().includes(this.recruiterFilter.toLowerCase())
    );
  }
  
  // Chart Data Methods
  getUserRoleChartData() {
    if (!this.analytics.totalUsers) return [];
    
    const candidates = this.analytics.totalCandidates || 0;
    const recruiters = this.analytics.totalRecruiters || 0;
    const admins = this.analytics.totalUsers - candidates - recruiters;
    
    return [
      { label: 'Candidates', value: candidates, color: '#3498db' },
      { label: 'Recruiters', value: recruiters, color: '#e74c3c' },
      { label: 'Admins', value: admins, color: '#f39c12' }
    ];
  }
  
  getActivityChartData() {
    return [
      { label: 'Jobs Posted', value: this.analytics.jobsPosted || 0, color: '#2ecc71' },
      { label: 'Total Applications', value: this.analytics.applicationsSubmitted || 0, color: '#3498db' },
      { label: 'Pending Applications', value: this.analytics.applicationsPending || 0, color: '#f39c12' },
      { label: 'Accepted Applications', value: this.analytics.applicationsAccepted || 0, color: '#27ae60' },
      { label: 'Rejected Applications', value: this.analytics.applicationsRejected || 0, color: '#e74c3c' },
      { label: 'Active Jobs', value: this.analytics.activeJobs || 0, color: '#16a085' }
    ];
  }
  
  getPieSlicePath(segment: any, index: number, data: any[]): string {
    const total = data.reduce((sum, item) => sum + item.value, 0);
    if (total === 0) return '';
    
    const percentage = segment.value / total;
    const angle = percentage * 2 * Math.PI;
    
    let startAngle = 0;
    for (let i = 0; i < index; i++) {
      startAngle += (data[i].value / total) * 2 * Math.PI;
    }
    
    const endAngle = startAngle + angle;
    const radius = 80;
    
    const x1 = Math.cos(startAngle) * radius;
    const y1 = Math.sin(startAngle) * radius;
    const x2 = Math.cos(endAngle) * radius;
    const y2 = Math.sin(endAngle) * radius;
    
    const largeArcFlag = angle > Math.PI ? 1 : 0;
    
    return `M 0 0 L ${x1} ${y1} A ${radius} ${radius} 0 ${largeArcFlag} 1 ${x2} ${y2} Z`;
  }
  
  getBarWidth(value: number, data: any[]): number {
    const maxValue = Math.max(...data.map(item => item.value));
    return maxValue > 0 ? (value / maxValue) * 100 : 0;
  }
  
  getActiveUsersCount(): number {
    if (!this.users || this.users.length === 0) return 0;
    return this.users.filter(user => user.isActive).length;
  }
  
  getInactiveUsersCount(): number {
    if (!this.users || this.users.length === 0) return 0;
    return this.users.filter(user => !user.isActive).length;
  }
}
