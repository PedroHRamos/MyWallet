using MyWalletWebAPI.Enum;

namespace MyWalletWebAPI.Domain;

public class AssetCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string HaveVariableValue { get; set; }
    public UpdateFrequencyEnum UpdateFrequency { get; set; }
    public ICollection<Asset> Assets { get; set; }

}
