using APIKanini.DTOs;
using APIKanini.Interface;
using APIKanini.Models;

namespace APIKanini.Service
{
    public class UserService
    {
        private readonly IHospitalAPI<User> _userRepo;
        private readonly IUser _user;
        public UserService(IHospitalAPI<User> usr,IUser user)
        {
            _userRepo = usr;
            _user = user;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _userRepo.GetByIdAsync(id);
        }
        public async Task<User> GetByNameAsync(string name)
        {
            return await _user.GetByUsernameAsync(name);
        }

        public async Task<User> AddAsync(User user)
        {
            return await _userRepo.AddAsync(user);
        }

        public async Task<User> UpdateAsync(string id,UsersDto dto)
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
