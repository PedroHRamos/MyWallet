using MyWalletWebAPI.Domain;
using MyWalletWebAPI.Requests;

namespace MyWalletWebAPI.Services.AssetCategoryService;

public interface IAssetCategoryService
{
    Task<AssetCategory> CreateAsync(CreateAssetCategoryRequest request);
    Task<IEnumerable<AssetCategory?>> GetAllAsync();
    Task<AssetCategory?> GetAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateAssetCategoryRequest request);
    Task<bool> DeleteAsync(Guid id);
}
