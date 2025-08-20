using APIKanini.Models;

namespace APIKanini.Interface
{
    public interface IToken
    {
        string GenerateToken(User user);
    }
}
