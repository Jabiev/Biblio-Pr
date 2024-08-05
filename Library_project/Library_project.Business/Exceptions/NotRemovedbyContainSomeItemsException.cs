namespace Library_project.Business.Exceptions;

public class NotRemovedbyContainSomeItemsException : Exception
{
    public NotRemovedbyContainSomeItemsException(string? message) : base(message)
    {
    }
}