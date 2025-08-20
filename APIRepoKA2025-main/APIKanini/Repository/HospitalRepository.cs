using APIKanini.Data;
using APIKanini.Interface;
using APIKanini.Models;
using Microsoft.EntityFrameworkCore;

namespace APIKanini.Repository
{
    public class HospitalRepository :IHospitalAPI<Hospital>
    {
        private readonly HospitalContext _context;

        public HospitalRepository(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _context.Hospitals.ToListAsync();
        }

        public async Task<Hospital?> GetByIdAsync(string id)
        {
            return await _context.Hospitals.FindAsync(id);
        }

        public async Task<Hospital> AddAsync(Hospital entity)
        {
            _context.Hospitals.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Hospital> UpdateAsync(Hospital entity)
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
