using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWallet.Features.Users;
using MyWallet.Models;

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
            var User = await _service.CreateAsync(request);
            _logger.LogInformation("POST /api/Users - User created: {Id}", User.Id);
            return CreatedAtAction(nameof(Get), new { id = User.Id }, User);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var Users = await _service.GetAllAsync();
            if (!Users.Any())
            {
                _logger.LogWarning("GET /api/Users - Users not found");
                return NotFound();
            }
            _logger.LogInformation("GET /api/Users - Finished");
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var User = await _service.GetAsync(id);
            if (User == null)
            {
                _logger.LogWarning("GET /api/Users/{id} - User not found", id);
                return NotFound();
            }
            _logger.LogInformation("GET /api/Users/{id} - Finished", id);
            return User;
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
