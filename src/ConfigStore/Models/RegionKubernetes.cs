namespace ConfigStore.Models;

public class RegionKubernetes : RegionBase
{
    public RegionKubernetes(string? name)
    {
        Name = name;
    }

    public string? Name { get; set; }
    
    public override void Validate()
    {
        if (Name is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}