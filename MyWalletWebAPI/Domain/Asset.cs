using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletWebAPI.Domain;

public class Asset
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public DateTime LastUpdated { get; set; }

    [ForeignKey("AssetCategoryId")]
    public Guid AssetCategoryId { get; set; }
    public AssetCategory AssetCategory { get; set; }
}
