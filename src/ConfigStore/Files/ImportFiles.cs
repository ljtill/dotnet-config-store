using ConfigStore.Models;

namespace ConfigStore.Files;

public static class ImportFiles
{
    public static List<Region> Invoke(string filePath)
    {
        BaseFiles.ValidateFile(BaseFiles.Operation.Import, filePath);

        var fileContents = File.ReadAllText(filePath);
        BaseFiles.ValidateFileContents(fileContents);
        
        var items = JsonSerializer.Deserialize<List<Region>>(fileContents, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        if (items is null)
        {
            throw new Exception("No items returned from deserialization");
        }

        return items;
    }
}