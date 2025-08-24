namespace Frontend.Models
{
    public class UserDto
    {
        public int UserId { get; set; }      // Primary Key
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
