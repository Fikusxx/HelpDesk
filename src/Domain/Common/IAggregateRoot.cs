
namespace Domain.Common;


public interface IAggregateRoot<TId> : IEntity<TId>, IHasDomainEvents
	where TId : IHasId
{ }

