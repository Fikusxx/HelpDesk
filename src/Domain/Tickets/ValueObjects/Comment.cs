using Domain.Common.Exceptions;

namespace Domain.Tickets.ValueObjects;

public sealed record Comment
{
	public string Value { get; private set; }
	public DateTimeOffset CreatedAt { get; private set; }

	private Comment(string value, DateTimeOffset createdAt)
	{
		this.Value = value;
		this.CreatedAt = createdAt;
	}

	public static Comment CreateNew(string comment, DateTimeOffset createdAt)
	{
		if (string.IsNullOrWhiteSpace(comment))
			throw new DomainException($"{nameof(comment)} can't be null or empty");

		if (createdAt > DateTimeOffset.UtcNow)
			throw new DomainException($"{nameof(createdAt)} can't be in the future");

		return new Comment(comment, createdAt);
	}
}
