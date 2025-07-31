using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWalletWebAPI.Domain;
using MyWalletWebAPI.Models.Database;
using MyWalletWebAPI.Repositories;
using MyWalletWebAPI.Requests;
using MyWalletWebAPI.Services.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.Users
{

    public class UserService : IUserService
    {
        private readonly MyWalletDbContext _context;
        private readonly IUsersRepository _repository;
        private readonly ILogger<UserService> _logger;
        public UserService(MyWalletDbContext context, ILogger<UserService> logger, IUsersRepository repository)
        {
            _context = context;
            _logger = logger;
            _repository = repository;
        }

        public async Task<User> CreateAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Cpf = request.Cpf,
                InvestorProfile = request.InvestorProfile,
                BirthDate = request.BirthDate,
                Plan = request.Plan
            };

            var userAlreadyExists = await _repository.ExistsByCpfAsync(user.Cpf);

            if(userAlreadyExists)
            {
                _logger.LogWarning("User with CPF {Cpf} already exists", user.Cpf);
                throw new ArgumentException($"User with CPF {user.Cpf} already exists.");
            }

            var userCreated = await _repository.CreateAsync(user);

            if (userCreated is null)
            {
                _logger.LogError("Failed to create user with CPF {Cpf}", user.Cpf);
                throw new InvalidOperationException("Failed to create user.");
            }

            _logger.LogInformation("User created with Id {Id}", userCreated.Id);
            return user;
        }

        public async Task<IEnumerable<User?>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            if (!users.Any())
                _logger.LogWarning("No users found");
            else
                _logger.LogInformation("Get users Finished");

            return users;
        }

        public async Task<User?> GetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);
            if (user == null)
                _logger.LogWarning("User not found: {Id}", id);
            else
                _logger.LogInformation("Get user {Id} finished", id);

            return user;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request)
        {
            var updateResult = await _repository.UpdateAsync(id, new User
            {
                Id = id,
                Name = request.Name,
                Cpf = request.Cpf,
                InvestorProfile = request.InvestorProfile,
                BirthDate = request.BirthDate,
                Plan = request.Plan
            });

            if(updateResult)
            {

                _logger.LogInformation("User updated: {Id}", id);
                return true;
            }

            _logger.LogWarning("User not found for update: {Id}", id);
            return false;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var hasDeleted = await _repository.DeleteAsync(id);

            if (hasDeleted)
            {
                _logger.LogInformation("User deleted: {Id}", id);
                return true;

                
            }

            _logger.LogWarning("User not found for deletion: {Id}", id);
            return false;

        }
    }
}
