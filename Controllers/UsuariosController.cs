using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWallet.Features.Usuarios;
using MyWallet.Models;

namespace MyWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(UsuarioService service, ILogger<UsuariosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] CreateUsuarioRequest request)
        {
            var usuario = await _service.CreateAsync(request);
            _logger.LogInformation("POST /api/usuarios - Usuário criado: {Id}", usuario.Id);
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(Guid id)
        {
            var usuario = await _service.GetAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("GET /api/usuarios/{id} - Usuário não encontrado", id);
                return NotFound();
            }
            _logger.LogInformation("GET /api/usuarios/{id} - Usuário retornado", id);
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            if (!updated)
            {
                _logger.LogWarning("PUT /api/usuarios/{id} - Usuário não encontrado para atualização", id);
                return NotFound();
            }
            _logger.LogInformation("PUT /api/usuarios/{id} - Usuário atualizado", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("DELETE /api/usuarios/{id} - Usuário não encontrado para exclusão", id);
                return NotFound();
            }
            _logger.LogInformation("DELETE /api/usuarios/{id} - Usuário excluído", id);
            return NoContent();
        }
    }
}
