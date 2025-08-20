using APIKanini.Models;

namespace APIKanini.DTOs
{
    public class UsersDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }      // Admin / Doctor / Patient
    }
}
