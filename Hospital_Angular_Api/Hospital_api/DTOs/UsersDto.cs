using Hospital_Management.Models;

namespace Hospital_Management.DTOs
{
    public class UsersDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public Role? Role { get; set; }
    }
}
