namespace Microsoft.ConfigStore.Commands;

public static class ExportCommand
{
    public static Command Create()
    {
        // Command
        var command = new Command("export", "");

        // Options
        var accountNameOption = new Option<string>("--account-name");
        accountNameOption.AddAlias("-n");
        command.AddOption(accountNameOption);

        var accountKeyOption = new Option<string>("--account-key");
        accountKeyOption.AddAlias("-k");
        command.AddOption(accountKeyOption);

        var filePathOption = new Option<string>("--file-path");
        filePathOption.AddAlias("-f");
        command.AddOption(filePathOption);

        // Handler
        command.SetHandler(async (string accountName, string accountKey, string filePath) =>
        {
            try
            {
                // Setup the database client
                var client = DatabaseClient.Create(accountName, accountKey);

                // Retrieve the items from cosmos db
                var items = await ExportItems.InvokeAsync(client);

                // Write the items to the file
                ExportFiles.Invoke(items, filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Application terminated");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception message - {0}", ex.Message);
            }
        }, accountNameOption, accountKeyOption, filePathOption);

        return command;
    }
}