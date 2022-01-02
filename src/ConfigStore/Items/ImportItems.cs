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
            foreach (var item in items)
            {
                // TODO: Check if item already exists
                await container.CreateItemAsync<Region>(item, new PartitionKey(item.location));
            }
        }
        catch (CosmosException ce)
        {
            Console.WriteLine($"Cosmos error encountered: {ce.Message}");
        }

        return;
    }
}