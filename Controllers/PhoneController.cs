using Microsoft.AspNetCore.Mvc;
using UserPhoneApp.DtoApp;
using UserPhoneApp.Repository;

namespace UserPhoneApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ILogger<PhonesController> _logger;
        public PhonesController(IPhoneRepository phoneRepository, ILogger<PhonesController> logger)
        {
            _phoneRepository = phoneRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhones()
        {
            try
            {
                var phones = await _phoneRepository.GetAllPhoneAsync();
                return Ok(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении всех телефонов.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoneById(Guid id)
        {
            try
            {
                var phone = await _phoneRepository.GetPhoneByIdAsync(id);
                if (phone == null)
                {
                    _logger.LogWarning("Телефон с id {Id} не найден.", id);
                    return NotFound();
                }
                return Ok(phone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении телефона с id {Id}.", id);
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhone([FromBody] PhoneDto phoneDto)
        {
            try
            {
                var phoneId = await _phoneRepository.CreatePhoneAsync(phoneDto);
                _logger.LogInformation("Телефон создан с id {Id}.", phoneId);
                return CreatedAtAction(nameof(GetPhoneById), new { id = phoneId }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при создании телефона.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhone(Guid id, [FromBody] UpdatePhoneDto updatePhoneDto)
        {
            try
            {
                var result = await _phoneRepository.UpdatePhoneAsync(id, updatePhoneDto);
                if (!result)
                {
                    _logger.LogWarning("Телефон с id {Id} не найден для обновления.", id);
                    return NotFound();
                }
                _logger.LogInformation("Телефон с id {Id} успешно обновлен.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при обновлении телефона с id {Id}.", id);
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(Guid id)
        {
            try
            {
                var result = await _phoneRepository.DeletePhoneAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Телефон с id {Id} не найден для удаления.", id);
                    return NotFound();
                }
                _logger.LogInformation("Телефон с id {Id} успешно удален.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при удалении телефона с id {Id}.", id);
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
