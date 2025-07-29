using Microsoft.EntityFrameworkCore;
using MyWalletWebAPI.Domain;
using MyWalletWebAPI.Models.Database;

namespace MyWalletWebAPI.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly MyWalletDbContext _context;
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(MyWalletDbContext context, ILogger<UsersRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User> CreateAsync(User user)
    {
        user.Id = Guid.NewGuid();
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User created with Id {Id}", user.Id);
        return user;
    }

    public async Task<IEnumerable<User?>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        if (!users.Any())
            _logger.LogWarning("No users found");
        else
            _logger.LogInformation("Get users Finished");
        return users;
    }

    public async Task<User?> GetAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            _logger.LogWarning("User not found: {Id}", id);
        else
            _logger.LogInformation("Get user {Id} finished", id);
        return user;
    }

    public async Task<bool> UpdateAsync(Guid id, User user)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            _logger.LogWarning("User not found for update: {Id}", id);
            return false;
        }

        existingUser.Name = user.Name;
        existingUser.Cpf = user.Cpf;
        existingUser.InvestorProfile = user.InvestorProfile;
        existingUser.BirthDate = user.BirthDate;
        existingUser.Plan = user.Plan;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User updated with Id {Id}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            _logger.LogWarning("User not found for deletion: {Id}", id);
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User deleted with Id {Id}", id);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var exists = await _context.Users.AnyAsync(u => u.Id == id);
        if (!exists)
            _logger.LogWarning("User does not exist: {Id}", id);
        else
            _logger.LogInformation("User exists: {Id}", id);
        return exists;
    }

    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        var exists = await _context.Users.AnyAsync(u => u.Cpf == cpf);
        if (!exists)
            _logger.LogWarning("User with CPF {Cpf} does not exist", cpf);
        else
            _logger.LogInformation("User with CPF {Cpf} exists", cpf);
        return exists;
    }

}