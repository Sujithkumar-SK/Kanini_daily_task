using APIKanini.Interface;
using APIKanini.Models;

namespace APIKanini.Service
{
    public class DoctorService
    {
        private readonly IHospitalAPI<Doctor> _doctorRepo;

        public DoctorService(IHospitalAPI<Doctor> doc)
        {
            _doctorRepo = doc;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _doctorRepo.GetAllAsync();
        }

        public async Task<Doctor> GetByIdAsync(string id)
        {
            return await _doctorRepo.GetByIdAsync(id);
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {

            return await _doctorRepo.AddAsync(doctor);
        }

        public async Task<Doctor> UpdateAsync(Doctor doctor)
        {
            return await _doctorRepo.UpdateAsync(doctor);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _doctorRepo.DeleteAsync(id);
        }
    }
}
