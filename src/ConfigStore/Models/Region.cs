namespace ConfigStore.Models;

public class Region : RegionBase
{
    public Region(string? id, string? location, string? resourceGroup, string? name, RegionProperties properties, RegionResources resources)
    {
        Id = id;
        Location = location;
        ResourceGroup = resourceGroup;
        Name = name;
        Properties = properties;
        Resources = resources;
    }
    
    public string? Id { get; }
    public string? Location { get; }
    public string? ResourceGroup { get; }
    public string? Name { get; }
    public RegionProperties Properties { get; }
    public RegionResources Resources { get;  } 
    
    public override void Validate()
    {
        if (Id is null || Location is null || Name is null || Properties is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}