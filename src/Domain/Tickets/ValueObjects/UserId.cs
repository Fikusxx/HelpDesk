using Domain.Common.Exceptions;
using Domain.Common;

namespace Domain.Tickets.ValueObjects;

public sealed record UserId : IHasId
{
	public Guid Value { get; private set; }

	private UserId(Guid value)
	{
		this.Value = value;
	}

	public static UserId CreateNew(Guid id)
	{
		if (id == Guid.Empty)
			throw new DomainException($"{nameof(UserId)} cannot have empty {nameof(id)}");

		return new UserId(id);
	}
}
