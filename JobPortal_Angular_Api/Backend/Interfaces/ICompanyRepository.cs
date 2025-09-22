using Backend.Models;

namespace Backend.Interfaces;

public interface ICompanyRepository
{
  Task<CompanyProfile?> GetByRecruiterIdAsync(int recruiterId);
  Task<CompanyProfile> AddAsync(CompanyProfile company);
  Task<CompanyProfile?> UpdateAsync(CompanyProfile company);
  Task<bool> SoftDeleteAsync(int companyProfileId);
}
