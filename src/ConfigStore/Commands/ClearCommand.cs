using ConfigStore.Clients;
using ConfigStore.Items;

namespace ConfigStore.Commands;

public static class ClearCommand
{
    public static Command Create()
    {
        // Command
        var command = new Command("clear", "");

        // Options
        var accountNameOption = new Option<string>("--account-name");
        accountNameOption.AddAlias("-n");
        command.AddOption(accountNameOption);

        var accountKeyOption = new Option<string>("--account-key");
        accountKeyOption.AddAlias("-k");
        command.AddOption(accountKeyOption);
        
        // Handler
        command.SetHandler(async (string? accountName, string? accountKey) =>
        {
            try
            {
                // Setup the database client
                var client = await DatabaseClient.CreateAsync(accountName, accountKey);

                // Clear items from Cosmos DB
                await ClearItems.InvokeAsync(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Application terminated");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception message - {0}", ex.Message);
            }
        }, accountNameOption, accountKeyOption);

        return command;
    }
}