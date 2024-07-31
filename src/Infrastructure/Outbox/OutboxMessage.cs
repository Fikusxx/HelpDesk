

namespace Infrastructure.Outbox;

public sealed class OutboxMessage
{
	public required Guid Id { get; init; }
	public required DateTimeOffset OccuredOn { get; init; }
	public required string ExecutedBy { get; init; }
	public required string Event { get; init; }
}
