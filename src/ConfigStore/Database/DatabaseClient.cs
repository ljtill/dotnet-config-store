namespace Microsoft.ConfigStore.Database;

public static class DatabaseClient
{
    private static string? _accountName;
    private static string? _accountKey;

    public static CosmosClient Create(string accountName, string accountKey)
    {
        Validate(accountName, accountKey);

        // Create cosmos client
        var client = new CosmosClient($"https://{_accountName}.documents.azure.com:443/", _accountKey);

        // Create cosmos container
        var database = client.GetDatabase("global");
        database.CreateContainerIfNotExistsAsync("regions", "/location").Wait();

        return client;
    }

    private static void Validate(string accountName, string accountKey)
    {
        // Account Name
        if (accountName is null)
        {
            var envAccountName = Environment.GetEnvironmentVariable("COSMOS_ACCOUNT_NAME");
            if (envAccountName is not null)
            {
                _accountName = envAccountName;
            }
            else
            {
                throw new Exception("COSMOS_ACCOUNT_NAME environment variable is not set.");
            }
        }
        else
        {
            _accountName = accountName;
        }

        // Account Key
        if (accountKey is null)
        {
            var envAccountKey = Environment.GetEnvironmentVariable("COSMOS_PRIMARY_KEY");
            if (envAccountKey is not null)
            {
                _accountKey = envAccountKey;
            }
            else
            {
                throw new Exception("COSMOS_PRIMARY_KEY environment variable is not set.");
            }
        }
        else
        {
            _accountKey = accountKey;
        }
    }
}