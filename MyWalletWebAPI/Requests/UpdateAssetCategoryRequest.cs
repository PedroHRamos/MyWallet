using MyWalletWebAPI.Enum;

namespace MyWalletWebAPI.Requests;

public class UpdateAssetCategoryRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool HaveVariableValue { get; set; }
    public UpdateFrequencyEnum UpdateFrequency { get; set; }
}