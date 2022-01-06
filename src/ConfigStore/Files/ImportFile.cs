using ConfigStore.Models;

namespace ConfigStore.Files;

public static class ImportFile
{
    public static List<Region> Invoke(string filePath)
    {
        BaseFile.ValidateFile(BaseFile.Operation.Import, filePath);

        var fileContents = File.ReadAllText(filePath);
        BaseFile.ValidateFileContents(fileContents);
        
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