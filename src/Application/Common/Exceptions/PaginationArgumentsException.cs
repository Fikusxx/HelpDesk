namespace Application.Common.Exceptions;

public sealed class PaginationArgumentsException : Exception
{
    public string Details { get; init; }

    public PaginationArgumentsException(string message) : base(message)
    {
        Details = message;
    }
}
