using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Services;

public class KidService : IKidService
{
  private readonly IKidRepository _repo;
  public KidService(IKidRepository repo)
  {
    _repo = repo;
  }
  public async Task<IEnumerable<Kid>> GetAllKids()
  {
    var temp = await _repo.GetAll();
    return temp;
  }
  public async Task<Kid> GetKidById(int id)
  {
    var temp = await _repo.GetById(id);
    return temp;
  }
  public async Task<bool> CreateKid(CreateKidDto dto)
  {
    var kid = new Kid
    {
      Name = dto.Name,
      Age = dto.Age,
      Grade = dto.Grade,
      ProfileImage = dto.ProfileImage,
      UserId = dto.UserId
    };
    return await _repo.Add(kid);
  }
  public async Task<bool> UpdateKid(int id, UpdateKidDto dto)
  {
    var temp = await _repo.GetById(id);
    if (temp == null) return false;
    temp.Name = dto.Name;
    temp.Age = dto.Age;
    temp.Grade = dto.Grade;
    temp.ProfileImage = dto.ProfileImage;
    return await _repo.Update(temp);
  }
  public async Task<bool> DeleteKid(int id)
  {
    var temp = await _repo.GetById(id);
    if (temp == null) return false;
    return await _repo.Delete(temp);
  }
}