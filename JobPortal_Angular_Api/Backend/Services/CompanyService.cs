using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Services;

public class CompanyService : ICompanyService
{
  private readonly ICompanyRepository _repo;

  public CompanyService(ICompanyRepository repo)
  {
    _repo = repo;
  }

  public async Task<CompanyProfileDto?> GetProfileAsync(int recruiterId)
  {
    var company = await _repo.GetByRecruiterIdAsync(recruiterId);
    if (company == null) return null;

    return new CompanyProfileDto
    {
      CompanyProfileId = company.CompanyId,
      CompanyName = company.Name,
      Description = company.Description ?? string.Empty,
      Website = company.Website,
      IsActive = company.IsActive
    };
  }

  public async Task<CompanyProfileDto> CreateProfileAsync(int recruiterId, CompanyProfileCreateDto dto)
  {
    var company = new CompanyProfile
    {
      UserId = recruiterId,
      Name = dto.CompanyName,
      Description = dto.Description,
      Website = dto.Website ?? string.Empty,
      IsActive = true
    };

    var created = await _repo.AddAsync(company);

    return new CompanyProfileDto
    {
      CompanyProfileId = created.CompanyId,
      CompanyName = created.Name,
      Description = created.Description ?? string.Empty,
      Website = created.Website,
      IsActive = created.IsActive
    };
  }

  public async Task<CompanyProfileDto?> UpdateProfileAsync(int recruiterId, CompanyProfileUpdateDto dto)
  {
    var company = await _repo.GetByRecruiterIdAsync(recruiterId);
    if (company == null) return null;

    if (!string.IsNullOrEmpty(dto.CompanyName)) company.Name = dto.CompanyName;
    if (!string.IsNullOrEmpty(dto.Description)) company.Description = dto.Description;
    if (!string.IsNullOrEmpty(dto.Website)) company.Website = dto.Website;
    if (dto.IsActive.HasValue) company.IsActive = dto.IsActive.Value;

    var updated = await _repo.UpdateAsync(company);

    return new CompanyProfileDto
    {
      CompanyProfileId = updated!.CompanyId,
      CompanyName = updated.Name,
      Description = updated.Description ?? string.Empty,
      Website = updated.Website,
      IsActive = updated.IsActive
    };
  }

  public async Task<bool> DeleteProfileAsync(int recruiterId)
  {
    var company = await _repo.GetByRecruiterIdAsync(recruiterId);
    if (company == null) return false;

    return await _repo.SoftDeleteAsync(company.CompanyId);
  }
}
