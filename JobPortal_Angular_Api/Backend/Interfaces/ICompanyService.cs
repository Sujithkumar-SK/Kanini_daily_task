using Backend.DTOs;

namespace Backend.Interfaces;

public interface ICompanyService
{
  Task<CompanyProfileDto?> GetProfileAsync(int recruiterId);
  Task<CompanyProfileDto> CreateProfileAsync(int recruiterId, CompanyProfileCreateDto dto);
  Task<CompanyProfileDto?> UpdateProfileAsync(int recruiterId, CompanyProfileUpdateDto dto);
  Task<bool> DeleteProfileAsync(int recruiterId);
}
