using Hospital_Management.Data;
using Hospital_Management.Interfaces;
//using Hospital_Management.Migrations;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repository
{
    public class HospitalRepository : IHospitalAPI<Hospital_Management.Models.Hospital>
    {
        private readonly HospitalContext _context;

        public HospitalRepository(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hospital_Management.Models.Hospital>> GetAllAsync()
        {
            return await _context.Hospitals.ToListAsync();
        }

        public async Task<Hospital_Management.Models.Hospital?> GetByIdAsync(string id)
        {
            return await _context.Hospitals.FindAsync(id);
        }

        public async Task<Hospital_Management.Models.Hospital> AddAsync(Hospital_Management.Models.Hospital hospital)
        {
            var count = await _context.Hospitals.CountAsync();
            hospital.HospitalId = $"HOS{(count + 1).ToString("D3")}";

            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();
            return hospital;
        }

        public async Task<Hospital_Management.Models.Hospital> UpdateAsync(Hospital_Management.Models.Hospital entity)
        {
            _context.Hospitals.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null) return false;
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
