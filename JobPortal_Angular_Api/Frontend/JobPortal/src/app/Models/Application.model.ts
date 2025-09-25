export interface Application {
  applicationId: number;
  jobId: number;
  candidateId: number;
  resumeId: number;
  status: string;
  appliedOn: Date;
  isActive: boolean;
  job?: any;
  resume?: any;
  candidate?: any;
  candidateName?: string;
  jobTitle?: string;
  resumeName?: string;
}