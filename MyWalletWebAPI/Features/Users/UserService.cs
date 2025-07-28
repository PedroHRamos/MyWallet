using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWallet.Models;

namespace MyWallet.Features.Users
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string InvestorProfile { get; set; }
        public DateTime BirthDate { get; set; }
        public int Plan { get; set; }
    }

    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string InvestorProfile { get; set; }
        public DateTime BirthDate { get; set; }
        public int Plan { get; set; }
    }

    public class UserService
    {
        private readonly MyWalletDbContext _context;
        private readonly ILogger<UserService> _logger;
        public UserService(MyWalletDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> CreateAsync(CreateUserRequest request)
        {
            var User = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Cpf = request.Cpf,
                InvestorProfile = request.InvestorProfile,
                BirthDate = request.BirthDate,
                Plan = request.Plan
            };
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User created with Id {Id}", User.Id);
            return User;
        }

        public async Task<IEnumerable<User?>> GetAllAsync()
        {
            var Users = await _context.Users.ToListAsync();
            if (!Users.Any())
                _logger.LogWarning("No users found");
            else
                _logger.LogInformation("Get users Finished");
            return Users;
        }

        public async Task<User?> GetAsync(Guid id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
                _logger.LogWarning("User not found: {Id}", id);
            else
                _logger.LogInformation("Get user {Id} finished", id);
            return User;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                _logger.LogWarning("User not found: {Id}", id);
                return false;
            }
            User.Name = request.Name;
            User.Cpf = request.Cpf;
            User.InvestorProfile = request.InvestorProfile;
            User.BirthDate = request.BirthDate;
            User.Plan = request.Plan;
            await _context.SaveChangesAsync();
            _logger.LogInformation("User updated: {Id}", id);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                _logger.LogWarning("User not found: {Id}", id);
                return false;
            }
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User deleted: {Id}", id);
            return true;
        }
    }
}
