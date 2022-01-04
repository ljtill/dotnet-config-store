using ConfigStore.Models;

namespace ConfigStore.Items;

public static class ClearItems
{
    private const string DatabaseName = "global";
    private const string ContainerName = "regions";

    public static async Task InvokeAsync(CosmosClient client)
    {
        var container = client.GetContainer(DatabaseName, ContainerName);

        try
        {
            using var feedIterator = container.GetItemQueryIterator<Region>("SELECT * FROM c");
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    await container.DeleteItemAsync<Region>(item.id, new PartitionKey(item.location));
                }
            }
        }
        catch (CosmosException ce)
        {
            Console.WriteLine("Cosmos error encountered: {0}", ce.Message);
        }
    }
}
