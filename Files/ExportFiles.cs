using System.Text.Json;
using Microsoft.ConfigStore.Schemas;

namespace Microsoft.ConfigStore.Files;

public static class ExportFiles
{
    public static void Invoke(List<Region> items, string filePath)
    {
        Validate(items, filePath);

        Console.WriteLine("Serializing items...");
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var itemsOutput = JsonSerializer.Serialize<List<Region>>(items, options);

        if (itemsOutput is null)
        {
            throw new Exception("Serialization failed");
        }

        File.WriteAllText(filePath, itemsOutput);
    }

    private static void Validate(List<Region> items, string filePath)
    {
        if (items.Count == 0)
        {
            throw new Exception("No items to export");
        }

        if (string.IsNullOrEmpty(filePath))
        {
            throw new Exception("File path is not set");
        }

        if (Path.GetExtension(filePath) != ".json")
        {
            throw new Exception("File extension must be .json");
        }
    }
}