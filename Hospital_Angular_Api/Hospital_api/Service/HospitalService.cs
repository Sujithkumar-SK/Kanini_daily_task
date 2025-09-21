using Hospital_Management.Interfaces;
using Hospital_Management.Models;

namespace Hospital_Management.Service
{
    public class HospitalService
    {
        private readonly IHospitalAPI<Hospital> _hospitalRepo;

        public HospitalService(IHospitalAPI<Hospital> hos)
        {
            _hospitalRepo = hos;
        }
        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _hospitalRepo.GetAllAsync();
        }

        public async Task<Hospital> GetByIdAsync(string id)
        {
            return await _hospitalRepo.GetByIdAsync(id);
        }

        public async Task<Hospital> AddAsync(Hospital hospital)
        {
            return await _hospitalRepo.AddAsync(hospital);
        }

        public async Task<Hospital> UpdateAsync(Hospital hospital)
        {
            return await _hospitalRepo.UpdateAsync(hospital);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _hospitalRepo.DeleteAsync(id);
        }
    }
}
