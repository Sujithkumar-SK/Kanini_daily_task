using Hospital_Management.Data;
using Hospital_Management.Interfaces;
//using Hospital_Management.Migrations;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repository
{
    public class PatientRepository:IHospitalAPI<Patient>
    {
        private readonly HospitalContext _context;

        public PatientRepository(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(string id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            // Count existing patients
            var count = await _context.Patients.CountAsync();

            // Generate new Id like PAT001, PAT002, ...
            patient.PatientId = $"PAT{(count + 1).ToString("D3")}";

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdateAsync(Patient entity)
        {
            _context.Patients.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
