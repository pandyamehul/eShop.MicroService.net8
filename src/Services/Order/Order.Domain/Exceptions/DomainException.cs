namespace Order.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) 
        : base($"Domain Exception: \"{message}\" raised from Domain Layer.")
    {
        //Domain Exception
    }
}