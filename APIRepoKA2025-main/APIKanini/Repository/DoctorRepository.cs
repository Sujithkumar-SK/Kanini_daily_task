using APIKanini.Data;
using APIKanini.DTOs;
using APIKanini.Interface;
using APIKanini.Models;
using Microsoft.EntityFrameworkCore;

namespace APIKanini.Repository
{
    public class DoctorRepository : IHospitalAPI<Doctor>
    {
        private readonly HospitalContext _context;

        public DoctorRepository(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(string id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<Doctor> AddAsync(Doctor doc)
        {
            // Generate new ID based on last Doctor in DB
            var lastDoctor = await _context.Doctors
                .OrderByDescending(d => d.DoctorId)
                .FirstOrDefaultAsync();

            string newId = lastDoctor == null
                ? "DOC001"
                : "DOC" + (int.Parse(lastDoctor.DoctorId.Substring(3)) + 1).ToString("D3");

            var doctor = new Doctor
            {
                DoctorId = newId,
                Name = doc.Name,
                Specialization = doc.Specialization,
                HospitalId = doc.HospitalId
            };

            await _context.AddAsync(doctor);
           await _context.SaveChangesAsync();
            return doctor;

        }

        public async Task<bool> DeleteAsync(string id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Doctor> UpdateAsync(Doctor entity)
        {
            _context.Doctors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
