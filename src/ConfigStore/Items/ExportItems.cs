namespace ConfigStore.Items;

public static class ExportItems
{
    private const string DatabaseName = "global";
    private const string ContainerName = "regions";

    public static async Task<List<Region>> InvokeAsync(CosmosClient client)
    {
        var items = new List<Region>();

        try
        {
            var container = client.GetContainer(DatabaseName, ContainerName);
            using var feedIterator = container.GetItemQueryIterator<Region>("SELECT * FROM c");
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    items.Add(item);
                }
            }
        }
        catch (CosmosException ce)
        {
            Console.WriteLine($"Cosmos error encountered: {ce.Message}");
        }

        return items;
    }
}