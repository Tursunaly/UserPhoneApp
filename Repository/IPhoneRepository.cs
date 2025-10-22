using UserPhoneApp.DtoApp;

namespace UserPhoneApp.Repository
{
    public interface IPhoneRepository
    {
        Task<Guid> CreatePhoneAsync(PhoneDto phoneDto);
        Task<PhoneDto?> GetPhoneByIdAsync(Guid id);
        Task<bool> UpdatePhoneAsync(Guid id, UpdatePhoneDto updatePhoneDto);
        Task<bool> DeletePhoneAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllPhoneAsync();

    }
}
