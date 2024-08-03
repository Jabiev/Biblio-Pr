namespace Library_project.Business.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string? message) : base(message)
    {
    }
}
