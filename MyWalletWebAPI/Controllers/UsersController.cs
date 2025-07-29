using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWallet.Services.Users;
using MyWallet.Models;
using MyWalletWebAPI.Requests;

namespace MyWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] CreateUserRequest request)
        {

            try
            {
                var user = await _service.CreateAsync(request);
                _logger.LogInformation("POST /api/Users - User created: {Id}", user.Id);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("POST /api/Users - User creation failed: {Message}", ex.Message);
                return BadRequest("Please check user data and try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("POST /api/Users - User creation failed: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
                throw;
            }

            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            if (!users.Any())
            {
                _logger.LogWarning("GET /api/Users - Users not found");
                return NotFound();
            }
            _logger.LogInformation("GET /api/Users - Finished");
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var user = await _service.GetAsync(id);
            if (user == null)
            {
                _logger.LogWarning("GET /api/Users/{id} - User not found", id);
                return NotFound();
            }
            _logger.LogInformation("GET /api/Users/{id} - Finished", id);
            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            if (!updated)
            {
                _logger.LogWarning("PUT /api/Users/{id} - User not found", id);
                return NotFound();
            }
            _logger.LogInformation("PUT /api/Users/{id} - Finished", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("DELETE /api/Users/{id} - User not found", id);
                return NotFound();
            }
            _logger.LogInformation("DELETE /api/Users/{id} - Finished", id);
            return NoContent();
        }
    }
}
