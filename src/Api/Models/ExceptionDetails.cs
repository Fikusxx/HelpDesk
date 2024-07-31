

namespace UsersAPI.Models;

public sealed class ExceptionDetails
{
	public required string ErrorType { get; init; }
	public required string ErrorMessage { get; init; }
	public required string TraceId { get; init; }
}