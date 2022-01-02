using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.ConfigStore.Schemas;

public class Region : IJsonOnDeserialized
{
    public string? id { get; set; }

    public string? location { get; set; }

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
    public string? resourceGroup { get; set; }

    public string? apiManagement { get; set; }

    public string? kubernetes { get; set; }

    void IJsonOnDeserialized.OnDeserialized() => Validate();

    private void Validate()
    {
        if (resourceGroup is null || apiManagement is null || kubernetes is null)
        {
            throw new JsonException("Missing json property in data file");
        }
    }
}
