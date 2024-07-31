using Domain.Common.Exceptions;
using Domain.Common;

namespace Domain.Tickets.ValueObjects;

public sealed record TicketId : IHasId
{
	public Guid Value { get; private set; }

	private TicketId(Guid value)
	{
		this.Value = value;
	}

	public static TicketId CreateNew(Guid id)
	{
		if (id == Guid.Empty)
			throw new DomainException($"{nameof(TicketId)} cannot have empty {nameof(id)}");

		return new TicketId(id);
	}
}
