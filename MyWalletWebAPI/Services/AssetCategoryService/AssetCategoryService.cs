using MyWallet.Services.Users;
using MyWalletWebAPI.Domain;
using MyWalletWebAPI.Models.Database;
using MyWalletWebAPI.Repositories;
using MyWalletWebAPI.Requests;

namespace MyWalletWebAPI.Services.AssetCategoryService;

public class AssetCategoryService : IAssetCategoryService
{
    private readonly MyWalletDbContext _context;
    private readonly IAssetCategoryRepository _repository;
    private readonly ILogger<AssetCategoryService> _logger;
    public AssetCategoryService(MyWalletDbContext context, ILogger<AssetCategoryService> logger, IAssetCategoryRepository repository)
    {
        _context = context;
        _logger = logger;
        _repository = repository;
    }

    public async Task<AssetCategory> CreateAsync(CreateAssetCategoryRequest request)
    {
        var assetCategory = new AssetCategory
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            HaveVariableValue = request.HaveVariableValue,
            UpdateFrequency = request.UpdateFrequency,
        };

        var assetCategoryCreated = await _repository.CreateAsync(assetCategory);

        if (assetCategoryCreated is null)
        {
            _logger.LogError("Failed to create Asset Category with name {Name}", assetCategory.Name);
            throw new InvalidOperationException("Failed to create Asset Category.");
        }

        _logger.LogInformation("User created with name {Name}", assetCategory.Name);
        return assetCategory;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var hasDeleted = await _repository.DeleteAsync(id);

        if (hasDeleted)
        {
            _logger.LogInformation("User deleted: {Id}", id);
            return true;
        }

        _logger.LogWarning("Asset Category not found for deletion: {Id}", id);
        return false;
    }

    public async Task<IEnumerable<AssetCategory?>> GetAllAsync()
    {
        var assetCategories = await _repository.GetAllAsync();
        if (!assetCategories.Any())
            _logger.LogWarning("No asset categories found");
        else
            _logger.LogInformation("Get asset categories Finished");

        return assetCategories;
    }

    public async Task<AssetCategory?> GetAsync(Guid id)
    {
        var assetCategory = await _repository.GetAsync(id);
        if (assetCategory == null)
            _logger.LogWarning("Asset category not found: {Id}", id);
        else
            _logger.LogInformation("Get asset category {Id} finished", id);

        return assetCategory;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAssetCategoryRequest request)
    {
        var updateResult = await _repository.UpdateAsync(id, new AssetCategory
        {
            Id = id,
            Name = request.Name,
            Description = request.Description,
            UpdateFrequency = request.UpdateFrequency,
            HaveVariableValue = request.HaveVariableValue
        });

        if (updateResult)
        {
            _logger.LogInformation("Asset category updated: {Id}", id);
            return true;
        }

        _logger.LogWarning("Asset category not found for update: {Id}", id);
        return false;
    }
}