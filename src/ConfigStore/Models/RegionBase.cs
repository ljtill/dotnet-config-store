namespace ConfigStore.Models;

public abstract class RegionBase : IJsonOnDeserialized
{
    void IJsonOnDeserialized.OnDeserialized() => Validate();

    public abstract void Validate();
}
