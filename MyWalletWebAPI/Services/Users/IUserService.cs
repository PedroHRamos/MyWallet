using MyWallet.Models;
using MyWallet.Services.Users;
using MyWalletWebAPI.Requests;

namespace MyWalletWebAPI.Services.Users;

public interface IUserService
{
    Task<User> CreateAsync(CreateUserRequest request);
    Task<IEnumerable<User?>> GetAllAsync();
    Task<User?> GetAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateUserRequest request);
    Task<bool> DeleteAsync(Guid id);
}
