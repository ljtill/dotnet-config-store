namespace ConfigStore.Models;

public class RegionResources : RegionBase
{
    public RegionResources(RegionApiManagement apiManagement, RegionKubernetes kubernetes)
    {
        ApiManagement = apiManagement;
        Kubernetes = kubernetes;
    }
    
    public RegionApiManagement ApiManagement { get; }
    public RegionKubernetes Kubernetes { get; }
    
    public override void Validate()
    {
        if (ApiManagement is null || Kubernetes is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}