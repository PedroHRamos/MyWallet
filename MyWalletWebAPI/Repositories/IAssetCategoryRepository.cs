using MyWalletWebAPI.Domain;

namespace MyWalletWebAPI.Repositories;

public interface IAssetCategoryRepository
{
    Task<AssetCategory> CreateAsync(AssetCategory assetCategory);
    Task<IEnumerable<AssetCategory?>> GetAllAsync();
    Task<AssetCategory?> GetAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, AssetCategory assetCategory);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
