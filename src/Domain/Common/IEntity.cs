

namespace Domain.Common;

public interface IEntity<TId> where TId : IHasId
{
	public TId Id { get; }
}
