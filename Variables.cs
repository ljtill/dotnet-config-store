using System;
using Microsoft.ConfigStore.Exceptions;

namespace Microsoft.ConfigStore;

public static class Variables
{
    public static void TestEnvironmentVariables()
    {
        var accountName = Environment.GetEnvironmentVariable("COSMOS_ACCOUNT_NAME");
        var accountKey = Environment.GetEnvironmentVariable("COSMOS_PRIMARY_KEY");
        var databaseName = Environment.GetEnvironmentVariable("COSMOS_DATABASE_NAME");

        if (accountName == null || accountKey == null)
        {
            throw new AccountException();
        }

        if (databaseName == null)
        {
            throw new DatabaseException();
        }
    }
}