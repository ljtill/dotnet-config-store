using ConfigStore.Commands;

var rootCommand = new RootCommand()
{
    Name = "azc",
    Description = "Azure Configuration Store"
};

rootCommand.AddCommand(ImportCommand.Create());
rootCommand.AddCommand(ExportCommand.Create());
rootCommand.AddCommand(ClearCommand.Create());

return rootCommand.InvokeAsync(args).Result;
