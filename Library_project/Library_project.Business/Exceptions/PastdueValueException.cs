namespace Library_project.Business.Exceptions;

public class PastdueValueException : Exception
{
    public PastdueValueException(string? message) : base(message)
    {
    }
}