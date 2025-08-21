using Backend.Models;
using Backend.DTOs;
namespace Backend.Interfaces;

public interface IKidService
{
  Task<IEnumerable<Kid>> GetAllKids();
  Task<Kid> GetKidById(int id);
  Task<bool> CreateKid(CreateKidDto kid);
  Task<bool> UpdateKid(int id, UpdateKidDto kid);
  Task<bool> DeleteKid(int id);
}