namespace FootballBuddy.Auth.Application.Exceptions;

public class InvalidCredentialsException : ApplicationException
{
    public InvalidCredentialsException() : base("Invalid email or password")
    {
        
    }
}