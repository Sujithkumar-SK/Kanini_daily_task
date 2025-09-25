export interface Job {
  jobId: number;
  title: string;
  description: string;
  location: string;
  employmentType: string;
  salary?: number;
  postedBy: number;
  postedOn: Date;
  isActive: boolean;
  recruiter?: any;
  jobSkills?: any[];
}