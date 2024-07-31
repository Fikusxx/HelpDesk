using System.Runtime.Serialization;
using HelpDeskAdminContracts.Common;

namespace Domain.Common;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
	where TId : class, IHasId
{
	private readonly List<IDomainEvent> events = new();

	protected void AddDomainEvent(IDomainEvent domainEvent)
	{
		events.Add(domainEvent);
	}

	[IgnoreDataMember]
	public IReadOnlyList<IDomainEvent> DomainEvents => events.AsReadOnly();

	public void ClearDomainEvents()
	{
		events.Clear();
	}
}
