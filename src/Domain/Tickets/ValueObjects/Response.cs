using Domain.Common.Exceptions;

namespace Domain.Tickets.ValueObjects;

public sealed record Response
{
	public string Value { get; private set; }

	private Response(string value)
	{
		this.Value = value;
	}

	public static Response CreateNew(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new DomainException($"{nameof(value)} can't be null or empty");

		return new Response(value);
	}
}
