namespace ConfigStore.Files;

public static class BaseFiles
{
    public enum Operation
    {
        Import,
        Export
    }
    
    public static void ValidateFile(Operation operation, string filePath)
    {
        if (operation == Operation.Import)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(filePath);
                throw new Exception("File does not exist");
            }
        }
        
        if (Path.GetExtension(filePath) != ".json")
        {
            throw new Exception("File extension must be json");
        }
    }
    
    public static void ValidateFileContents(string fileContents)
    {
        if (fileContents is null)
        {
            throw new Exception("File contents are empty");
        }
    }
}