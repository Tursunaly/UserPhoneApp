using UserPhoneApp.Database;
using UserPhoneApp.DtoApp;

namespace UserPhoneApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<UserDto>> GetAllUsersAsync()
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
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            return new UserDto
            {
                id = user.id,
                name = user.name,
                email = user.email,
                DateBirth = user.DateBirth
            };
        }

        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.name = updateUserDto.name;
            user.email = updateUserDto.email;
            user.DateBirth = updateUserDto.DateBirth;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Guid> CreateUserAsync(UserDto userDto)
        {
            var user = new Models.User
            {
                id = Guid.NewGuid(),
                name = userDto.name,
                email = userDto.email,
                DateBirth = userDto.DateBirth
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.id;
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
