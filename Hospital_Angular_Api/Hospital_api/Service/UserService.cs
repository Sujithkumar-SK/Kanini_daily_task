using Hospital_Management.DTOs;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APIKanini.Service
{
    public class UserService
    {
        private readonly IHospitalAPI<User> _userRepo;
        private readonly IUser _userRepoSpecial;
        private readonly IPasswordHasher<User> _hasher;

        public UserService(
            IHospitalAPI<User> userRepo,
            IUser userRepoSpecial,
            IPasswordHasher<User> hasher)
        {
            _userRepo = userRepo;
            _userRepoSpecial = userRepoSpecial;
            _hasher = hasher;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userRepoSpecial.GetByUsernameAsync(username);
        }

        public async Task<User> AddAsync(User user, string password)
        {
            user.PasswordHash = _hasher.HashPassword(user, password);
            return await _userRepo.AddAsync(user);
        }

        public async Task<User> UpdateAsync(string id, UsersDto dto)
        {
            // Get the existing user from DB
            var usr = await _userRepo.GetByIdAsync(id);


            // Update fields from DTO
            usr.UserName = dto.Username;
            usr.PasswordHash = dto.Password; // hash if needed
            usr.Role = dto.Role;

            // Save changes
            await _userRepo.UpdateAsync(usr);
            return usr;
        }


        public async Task<bool> DeleteAsync(string id)
        {
            return await _userRepo.DeleteAsync(id);
        }
    }
}
