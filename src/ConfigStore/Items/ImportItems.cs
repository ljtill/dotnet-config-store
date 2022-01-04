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

        try
        {
            foreach (var item in items)
            {
                var itemExists = container.GetItemQueryIterator<Region>($"SELECT * FROM c WHERE c.id = {item.id}")
                    .IsNull();
                
                if (itemExists)
                {
                    await container.CreateItemAsync(item, new PartitionKey(item.location));    
                }
            }
        }
        catch (CosmosException ce)
        {
            Console.WriteLine($"Cosmos error encountered: {ce.Message}");
        }
    }
}