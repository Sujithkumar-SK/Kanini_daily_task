using Hospital_Management.DTOs;
using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IHospitalAPI<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);   // ✅ entity only
        Task<bool> DeleteAsync(string id);
    }

    public interface IUser
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
