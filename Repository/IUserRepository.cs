using UserPhoneApp.DtoApp;

namespace UserPhoneApp.Repository
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(UserDto userDto);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<bool> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();


    }
}
