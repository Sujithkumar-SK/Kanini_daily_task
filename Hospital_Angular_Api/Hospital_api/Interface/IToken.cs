using Hospital_Management.Models;

namespace Hospital_Management.Interface
{
    public interface IToken
    {
        string GenerateToken(User user);
    }
}
