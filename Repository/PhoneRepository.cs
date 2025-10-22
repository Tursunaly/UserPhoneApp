using Microsoft.AspNetCore.Mvc;
using UserPhoneApp.Database;
using UserPhoneApp.DtoApp;

namespace UserPhoneApp.Repository
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly AppDbContext _context;
        public PhoneRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public Task<IEnumerable<UserDto>> GetAllPhoneAsync()
        {
                       var users = _context.Users.Select(user => new UserDto
            {
                id = user.id,
                name = user.name,
                email = user.email,
                DateBirth = user.DateBirth
            });
            return Task.FromResult(users.AsEnumerable());
        }

        public async Task<PhoneDto?> GetPhoneByIdAsync(Guid id)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null) return null;
            return new PhoneDto
            {
                id = phone.id,
                number = phone.number,
                userId = phone.userId
            };
        }

        public async Task<Guid> CreatePhoneAsync(PhoneDto phoneDto)
        {
            var phone = new Models.Phone
            {
                id = Guid.NewGuid(),
                number = phoneDto.number,
                userId = phoneDto.userId
            };
            _context.Phones.Add(phone);
            await _context.SaveChangesAsync();
            return phone.id;
        }
        public async Task<bool> UpdatePhoneAsync(Guid id, UpdatePhoneDto updatePhoneDto)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null) return false;
            phone.number = updatePhoneDto.number;
            phone.userId = updatePhoneDto.userId;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePhoneAsync(Guid id)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null) return false;
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
