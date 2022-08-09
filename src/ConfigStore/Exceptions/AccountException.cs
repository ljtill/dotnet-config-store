namespace ConfigStore.Exceptions;

public class AccountException : Exception
{
    public AccountException() : base("Account environment variables is not set.")
    {
    }
}