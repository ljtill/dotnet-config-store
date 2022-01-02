using Microsoft.Azure.Cosmos;

namespace Microsoft.ConfigStore.Database;

public static class DatabaseClient
{
    public static CosmosClient Create(string accountName, string accountKey)
    {
        // Account Name
        if (accountName is null)
        {
            var envAccountName = Environment.GetEnvironmentVariable("COSMOS_ACCOUNT_NAME");
            if (envAccountName is not null)
            {
                accountName = envAccountName;
            }
            else
            {
                throw new Exception("COSMOS_ACCOUNT_NAME environment variable is not set.");
            }
        }

        // Account Key
        if (accountKey is null)
        {
            var envAccountKey = Environment.GetEnvironmentVariable("COSMOS_PRIMARY_KEY");
            if (envAccountKey is not null)
            {
                accountKey = envAccountKey;
            }
            else
            {
                throw new Exception("COSMOS_PRIMARY_KEY environment variable is not set.");
            }
        }

        // Create cosmos client
        var client = new CosmosClient($"https://{accountName}.documents.azure.com:443/", accountKey);

        // Create cosmos container
        var database = client.GetDatabase("global");
        database.CreateContainerIfNotExistsAsync("regions", "/location").Wait();

        return client;
    }
}