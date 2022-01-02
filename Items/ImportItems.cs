using Microsoft.Azure.Cosmos;
using Microsoft.ConfigStore.Schemas;

namespace Microsoft.ConfigStore.Items;

public static class ImportItems
{
    private static string _databaseName = "global";
    private static string _containerName = "regions";

    public static async Task InvokeAsync(CosmosClient client, List<Region> items)
    {
        var database = client.GetDatabase(_databaseName);
        var container = database.GetContainer(_containerName);

        if (container is null)
        {
            throw new Exception("Container not found");
        }

        try
        {
            Console.WriteLine("Creating items...");
            foreach (var item in items)
            {
                // TODO: Check if item already exists
                await container.CreateItemAsync<Region>(item, new PartitionKey(item.location));
            }
        }
        catch (CosmosException ce)
        {
            // TODO: Exception handling
            Console.WriteLine($"Cosmos error encountered: {ce.Message}");
        }

        return;
    }
}