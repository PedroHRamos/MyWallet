using Microsoft.AspNetCore.Mvc;
using MyWallet.Controllers;
using MyWallet.Services.Users;
using MyWalletWebAPI.Domain;
using MyWalletWebAPI.Requests;
using MyWalletWebAPI.Services.AssetCategoryService;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MyWalletWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetCategoryController : ControllerBase
{
    private readonly IAssetCategoryService _service;
    private readonly ILogger<AssetCategoryController> _logger;

    public AssetCategoryController(IAssetCategoryService service, ILogger<AssetCategoryController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<AssetCategory>> Create([FromBody] CreateAssetCategoryRequest request)
    {

        try
        {
            var assetCategory = await _service.CreateAsync(request);
            _logger.LogInformation("POST /api/AssetCategory - Asset category created: {Id}", assetCategory.Id);
            return CreatedAtAction(nameof(Get), new { id = assetCategory.Id }, assetCategory);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("POST /api/AssetCategory - Asset category creation failed: {Message}", ex.Message);
            return BadRequest("Please check user data and try again.");
        }
        catch (Exception ex)
        {
            _logger.LogError("POST /api/AssetCategory - Asset category creation failed: {Message}", ex.Message);
            return StatusCode(500, "Internal server error");
            throw;
        }


    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetCategory>>> GetAll()
    {
        var assetCategories = await _service.GetAllAsync();
        if (!assetCategories.Any())
        {
            _logger.LogWarning("GET /api/AssetCategory - asset categories not found");
            return NotFound();
        }
        _logger.LogInformation("GET /api/AssetCategory - Finished");
        return Ok(assetCategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetCategory>> Get(Guid id)
    {
        var assetCategory = await _service.GetAsync(id);
        if (assetCategory == null)
        {
            _logger.LogWarning("GET /api/AssetCategory/{id} - asset category not found", id);
            return NotFound();
        }
        _logger.LogInformation("GET /api/AssetCategory/{id} - Finished", id);
        return assetCategory;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAssetCategoryRequest request)
    {
        var updated = await _service.UpdateAsync(id, request);
        if (!updated)
        {
            _logger.LogWarning("PUT /api/AssetCategory/{id} - asset category not found", id);
            return NotFound();
        }
        _logger.LogInformation("PUT /api/AssetCategory/{id} - Finished", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("DELETE /api/AssetCategory/{id} - asset category not found", id);
            return NotFound();
        }
        _logger.LogInformation("DELETE /api/AssetCategory/{id} - Finished", id);
        return NoContent();
    }
}