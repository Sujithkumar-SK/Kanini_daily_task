using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class CompanyRepository : ICompanyRepository
{
  private readonly JobPortalContext _context;

  public CompanyRepository(JobPortalContext context)
  {
    _context = context;
  }

  public async Task<CompanyProfile?> GetByRecruiterIdAsync(int recruiterId)
  {
    return await _context.CompanyProfiles
        .FirstOrDefaultAsync(c => c.UserId == recruiterId && c.IsActive);
  }

  public async Task<CompanyProfile> AddAsync(CompanyProfile company)
  {
    _context.CompanyProfiles.Add(company);
    await _context.SaveChangesAsync();
    return company;
  }

  public async Task<CompanyProfile?> UpdateAsync(CompanyProfile company)
  {
    _context.CompanyProfiles.Update(company);
    await _context.SaveChangesAsync();
    return company;
  }

  public async Task<bool> SoftDeleteAsync(int companyProfileId)
  {
    var company = await _context.CompanyProfiles.FindAsync(companyProfileId);
    if (company == null) return false;

    company.IsActive = false;
    _context.CompanyProfiles.Update(company);
    await _context.SaveChangesAsync();
    return true;
  }
}
