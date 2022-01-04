namespace ConfigStore.Models;

public class Region : IJsonOnDeserialized
{
    public string? id { get; set; }
    public string? location { get; set; }
    public string? resourceGroup { get; set; }
    public string? name { get; set; }
    public RegionProperties? properties { get; set; }

    void IJsonOnDeserialized.OnDeserialized() => Validate();

    private void Validate()
    {
        if (id is null || location is null || name is null || properties is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}

public class RegionProperties : IJsonOnDeserialized
{
    public RegionApiManagement? apiManagement { get; set; }
    public RegionKubernetes? kubernetes { get; set; }

    void IJsonOnDeserialized.OnDeserialized() => Validate();

    private void Validate()
    {
        if (apiManagement is null || kubernetes is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}

public class RegionApiManagement : IJsonOnDeserialized
{
    public string? name { get; set; }

    void IJsonOnDeserialized.OnDeserialized() => Validate();

    private void Validate()
    {
        if (name is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}

public class RegionKubernetes : IJsonOnDeserialized
{
    public string? name { get; set; }

    void IJsonOnDeserialized.OnDeserialized() => Validate();

    private void Validate()
    {
        if (name is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}