using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWallet.Models;

namespace MyWallet.Features.Usuarios
{
    public class CreateUsuarioRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string InvestorProfile { get; set; }
        public DateTime BirthDate { get; set; }
        public int Plan { get; set; }
    }

    public class UpdateUsuarioRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string InvestorProfile { get; set; }
        public DateTime BirthDate { get; set; }
        public int Plan { get; set; }
    }

    public class UsuarioService
    {
        private readonly MyWalletDbContext _context;
        private readonly ILogger<UsuarioService> _logger;
        public UsuarioService(MyWalletDbContext context, ILogger<UsuarioService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Usuario> CreateAsync(CreateUsuarioRequest request)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Cpf = request.Cpf,
                InvestorProfile = request.InvestorProfile,
                BirthDate = request.BirthDate,
                Plan = request.Plan
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Usuário criado com Id {Id}", usuario.Id);
            return usuario;
        }

        public async Task<IEnumerable<Usuario?>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            if (!usuarios.Any())
                _logger.LogWarning("Nenhum usuario encontrado");
            else
                _logger.LogInformation("Usuários recuperados");
            return usuarios;
        }

        public async Task<Usuario?> GetAsync(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                _logger.LogWarning("Usuário não encontrado: {Id}", id);
            else
                _logger.LogInformation("Usuário recuperado: {Id}", id);
            return usuario;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUsuarioRequest request)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário para atualização não encontrado: {Id}", id);
                return false;
            }
            usuario.Name = request.Name;
            usuario.Cpf = request.Cpf;
            usuario.InvestorProfile = request.InvestorProfile;
            usuario.BirthDate = request.BirthDate;
            usuario.Plan = request.Plan;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Usuário atualizado: {Id}", id);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário para exclusão não encontrado: {Id}", id);
                return false;
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Usuário excluído: {Id}", id);
            return true;
        }
    }
}
