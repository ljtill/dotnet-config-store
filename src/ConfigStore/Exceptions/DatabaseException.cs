namespace Microsoft.ConfigStore.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException() : base("Database environment variable is not set.")
    {
    }
}
