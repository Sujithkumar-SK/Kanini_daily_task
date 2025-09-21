using Backend.Models;

namespace Backend.Interfaces;

public interface IToken
{
  string GenerateToken(User user);
}

