
// Command
var rootCommand = new RootCommand()
{
    Name = "azc",
    Description = "Azure Configuration CLI"
};

rootCommand.AddCommand(ImportCommand.Create());
rootCommand.AddCommand(ExportCommand.Create());
rootCommand.AddCommand(ClearCommand.Create());

return rootCommand.InvokeAsync(args).Result;
