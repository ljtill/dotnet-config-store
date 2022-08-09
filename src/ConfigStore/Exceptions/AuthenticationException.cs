namespace ConfigStore.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException() : base("Invalid account credentials.")
    {}
}