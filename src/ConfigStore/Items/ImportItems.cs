using ConfigStore.Models;

namespace ConfigStore.Items;

public static class ImportItems
{
    private const string DatabaseName = "global";
    private const string ContainerName = "regions";

    public static async Task InvokeAsync(CosmosClient client, List<Region> items)
    {
        var database = client.GetDatabase(DatabaseName);
        var container = database.GetContainer(ContainerName);

        if (container is null)
        {
            throw new Exception("Container not found");
        }

        foreach (var item in items)
        {
            try
            {
                await container.ReadItemAsync<Region>(item.Id, new PartitionKey("/location"));
            }
            catch (CosmosException ce)
            {
                if (ce.StatusCode == HttpStatusCode.NotFound)
                {
                    await container.CreateItemAsync<Region>(item, new PartitionKey(item.Location)); 
                }
                else
                {
                    throw new Exception($"Cosmos exception: {ce.Message}");
                }
            }
        }
    }
}