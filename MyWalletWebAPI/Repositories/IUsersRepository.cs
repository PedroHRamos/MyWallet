using MyWalletWebAPI.Domain;

namespace MyWalletWebAPI.Repositories;

public interface IUsersRepository
{
    Task<User> CreateAsync(User user);
    Task<IEnumerable<User?>> GetAllAsync();
    Task<User?> GetAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, User user);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByCpfAsync(string cpf);
}