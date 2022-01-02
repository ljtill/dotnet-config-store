namespace Microsoft.ConfigStore.Files;

public static class ImportFiles
{
    public static List<Region> Invoke(string filePath)
    {
        Validate(filePath);

        var fileContents = File.ReadAllText(filePath);

        if (fileContents is null)
        {
            throw new Exception("File contents are empty");
        }

        var items = JsonSerializer.Deserialize<List<Region>>(fileContents);

        if (items is null)
        {
            throw new Exception("No items returned from deserialization");
        }

        return items;
    }

    private static void Validate(string filePath)
    {
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