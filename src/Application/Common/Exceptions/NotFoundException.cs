namespace Application.Common.Exceptions;

public sealed class NotFoundException : Exception
{
    public string Details { get; init; }

    public NotFoundException(string message) : base(message)
    {
        Details = message;
    }
}
