using System.CommandLine;
using Microsoft.ConfigStore.Database;
using Microsoft.ConfigStore.Files;
using Microsoft.ConfigStore.Items;

// Options
var accountNameOption = new Option<string>("--account-name");
accountNameOption.AddAlias("-n");

var accountKeyOption = new Option<string>("--account-key");
accountKeyOption.AddAlias("-k");

var filePathOption = new Option<string>("--file-path");
filePathOption.AddAlias("-p");

// Commands : Export
var exportCommand = new Command("export", "");
exportCommand.AddOption(accountNameOption);
exportCommand.AddOption(accountKeyOption);
exportCommand.AddOption(filePathOption);
exportCommand.SetHandler(async (string accountName, string accountKey, string filePath) =>
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

// Commands : Import
var importCommand = new Command("import", "");
importCommand.AddOption(accountNameOption);
importCommand.AddOption(accountKeyOption);
importCommand.AddOption(filePathOption);
importCommand.SetHandler(async (string accountName, string accountKey, string filePath) =>
{
    try
    {
        // Setup the database client
        var client = DatabaseClient.Create(accountName, accountKey);

        // Parse the user provided file
        var items = ImportFiles.Invoke(filePath);

        // Push items to cosmos db
        await ImportItems.InvokeAsync(client, items);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Application terminated");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exception message - {0}", ex.Message);
    }
}, accountNameOption, accountKeyOption, filePathOption);

// Commands : Clear
var clearCommand = new Command("clear", "");
clearCommand.AddOption(accountNameOption);
clearCommand.AddOption(accountKeyOption);
clearCommand.SetHandler(async (string accountName, string accountKey) =>
{
    try
    {
        // Clear items from Cosmos DB
        var client = DatabaseClient.Create(accountName, accountKey);
        await ClearItems.InvokeAsync(client);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Application terminated");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exception message - {0}", ex.Message);
    }
}, accountNameOption, accountKeyOption);

// Commands : Root
var rootCommand = new RootCommand()
{
    Name = "azc",
    Description = "Azure Configuration CLI"
};

rootCommand.AddCommand(importCommand);
rootCommand.AddCommand(exportCommand);
rootCommand.AddCommand(clearCommand);

return rootCommand.InvokeAsync(args).Result;
