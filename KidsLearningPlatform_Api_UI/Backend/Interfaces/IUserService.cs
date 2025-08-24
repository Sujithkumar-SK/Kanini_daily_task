using Backend.DTOs;

public interface IUserService
{
    Task<UserDto?> Register(RegisterUserDto dto);
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<UserDto?> GetById(int id);
    Task<bool> DeleteUser(int id);
}
