namespace Library_project.Business.Exceptions;

public class NullorEmptyException : Exception
{
    public NullorEmptyException(string? message) : base(message)
    {
    }
}