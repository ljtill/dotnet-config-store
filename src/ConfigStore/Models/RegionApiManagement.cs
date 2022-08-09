namespace ConfigStore.Models;

public class RegionApiManagement : RegionBase
{
    public RegionApiManagement(string? name)
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