using Hospital_Management.Interfaces;
using Hospital_Management.Models;

namespace Hospital_Management.Service
{
    public class PatientService
    {
        private readonly IHospitalAPI<Patient> _patientRepo;

        public PatientService(IHospitalAPI<Patient> hos)
        {
            _patientRepo = hos;
        }
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _patientRepo.GetAllAsync();
        }

        public async Task<Patient> GetByIdAsync(string id)
        {
            return await _patientRepo.GetByIdAsync(id);
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            return await _patientRepo.AddAsync(patient);
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            return await _patientRepo.UpdateAsync(patient);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _patientRepo.DeleteAsync(id);
        }
    }
}
