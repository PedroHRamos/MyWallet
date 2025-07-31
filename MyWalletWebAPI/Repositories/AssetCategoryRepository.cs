using Microsoft.EntityFrameworkCore;
using MyWalletWebAPI.Domain;
using MyWalletWebAPI.Models.Database;

namespace MyWalletWebAPI.Repositories;

public class AssetCategoryRepository : IAssetCategoryRepository
{
    private readonly MyWalletDbContext _context;
    private readonly ILogger<AssetCategoryRepository> _logger;

    public AssetCategoryRepository(MyWalletDbContext context, ILogger<AssetCategoryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<AssetCategory> CreateAsync(AssetCategory assetCategory)
    {
        assetCategory.Id = Guid.NewGuid();
        _context.AssetCategories.Add(assetCategory);

        await _context.SaveChangesAsync();
        _logger.LogInformation("Asset Category created with Id {Id}", assetCategory.Id);

        return assetCategory;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var assetCategory = await _context.AssetCategories.FindAsync(id);
        if (assetCategory == null)
        {
            _logger.LogWarning("Asset Category not found for deletion: {Id}", id);
            return false;
        }

        _context.AssetCategories.Remove(assetCategory);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Asset Category deleted with Id {Id}", id);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var exists = await _context.AssetCategories.AnyAsync(a => a.Id == id);

        if (!exists)
            _logger.LogWarning("Asset Category does not exist: {Id}", id);
        else
            _logger.LogInformation("Asset Category exists: {Id}", id);

        return exists;
    }

    public async Task<IEnumerable<AssetCategory?>> GetAllAsync()
    {
        var assetCategories = await _context.AssetCategories.ToListAsync();

        if (!assetCategories.Any())
            _logger.LogWarning("No Asset Categories found");
        else
            _logger.LogInformation("Get Asset Categories Finished");

        return assetCategories;
    }

    public async Task<AssetCategory?> GetAsync(Guid id)
    {
        var assetCategories = await _context.AssetCategories.FindAsync(id);
        if (assetCategories == null)
            _logger.LogWarning("Asset Category not found: {Id}", id);
        else
            _logger.LogInformation("Get Asset Category {Id} finished", id);
        return assetCategories;
    }

    public async Task<bool> UpdateAsync(Guid id, AssetCategory assetCategory)
    {
        var existingAssetCategory = await _context.AssetCategories.FindAsync(id);
        if (existingAssetCategory == null)
        {
            _logger.LogWarning("User not found for update: {Id}", id);
            return false;
        }

        existingAssetCategory.Name = assetCategory.Name;
        existingAssetCategory.Description = assetCategory.Description;
        existingAssetCategory.HaveVariableValue = assetCategory.HaveVariableValue;
        existingAssetCategory.UpdateFrequency = assetCategory.UpdateFrequency;

        _context.AssetCategories.Update(existingAssetCategory);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Asset Category updated with Id {Id}", id);
        return true;
    }
}