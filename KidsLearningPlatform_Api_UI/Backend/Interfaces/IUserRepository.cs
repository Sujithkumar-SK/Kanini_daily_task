using Backend.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Interfaces;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetAll();
  Task<User> GetByEmail(string email);
  Task<User> GetById(int id);
  Task<bool> Add(User kid);
  Task<bool> Update(User kid);
  Task<bool> Delete(User kid);
}