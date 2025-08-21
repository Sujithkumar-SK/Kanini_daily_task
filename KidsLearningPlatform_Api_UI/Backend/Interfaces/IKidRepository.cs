using Backend.Models;

namespace Backend.Interfaces;

public interface IKidRepository
{
  Task<IEnumerable<Kid>> GetAll();
  Task<Kid> GetById(int id);
  Task<bool> Add(Kid kid);
  Task<bool> Update(Kid kid);
  Task<bool> Delete(Kid kid);
}