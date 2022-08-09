namespace ConfigStore.Models;

public class RegionProperties : RegionBase
{
    public RegionProperties(int? deploymentRing)
    {
        DeploymentRing = deploymentRing;
    }
    
    public int? DeploymentRing { get; }

    public override void Validate()
    {
        if (DeploymentRing is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}