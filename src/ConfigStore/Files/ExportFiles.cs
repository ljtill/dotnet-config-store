using ConfigStore.Models;

namespace ConfigStore.Files;

public static class ExportFiles
{
    public static void Invoke(List<Region> items, string filePath)
    {
        BaseFiles.ValidateFile(BaseFiles.Operation.Export, filePath);
        
        if (items.Count == 0)
        {
            throw new Exception("No items to export");
        }
        
        var itemsOutput = JsonSerializer.Serialize<List<Region>>(items, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        if (itemsOutput is null)
        {
            throw new Exception("Serialization failed");
        }

        File.WriteAllText(filePath, itemsOutput);
    }
}