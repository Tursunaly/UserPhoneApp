using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserPhoneApp.DtoApp;
using UserPhoneApp.Repository;
namespace UserPhoneApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении всех пользователей.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("Пользователь с id {Id} не найден.", id);
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении пользователя с id {Id}.", id);
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var userId = await _userRepository.CreateUserAsync(userDto);
                _logger.LogInformation("Пользователь создан с id {Id}.", userId);
                return CreatedAtAction(nameof(GetUserById), new { id = userId }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при создании пользователя.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var result = await _userRepository.UpdateUserAsync(id, updateUserDto);
                if (!result)
                {
                    _logger.LogWarning("Пользователь с id {Id} не найден для обновления.", id);
                    return NotFound();
                }
                _logger.LogInformation("Пользователь с id {Id} успешно обновлен.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при обновлении пользователя с id {Id}.", id);
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userRepository.DeleteUserAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Пользователь с id {Id} не найден для удаления.", id);
                    return NotFound();
                }
                _logger.LogInformation("Пользователь с id {Id} успешно удален.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при удалении пользователя с id {Id}.", id);
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
