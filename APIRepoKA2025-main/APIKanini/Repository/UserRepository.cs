using APIKanini.Data;
using APIKanini.Interface;
using APIKanini.Models;
using Microsoft.EntityFrameworkCore;

namespace APIKanini.Repository
{
    public class UserRepository :IHospitalAPI<User>,IUser
    {
        private readonly HospitalContext _context;

        public UserRepository(HospitalContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(r=>r.Role).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(r=>r.Role).FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<User> AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
