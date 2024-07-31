using System.Runtime.Serialization;

namespace Domain.Common;


public abstract class Entity<TId> : IEntity<TId> where TId : class, IHasId
{
	public TId Id { get; protected set; } = null!;

	[IgnoreDataMember]
	public Guid EntityId
	{
		get => Id.Value;
		private set { }
	}

	protected void CheckRule(IBusinessRule rule)
	{
		if (rule.IsBroken())
		{
			throw new BusinessRuleValidationException(rule);
		}
	}

	public override bool Equals(object? obj)
	{
		return obj is Entity<TId> entity && Id.Equals(entity.Id);
	}

	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}

	public static bool operator ==(Entity<TId> a, Entity<TId> b)
	{
		return Equals(a, b);
	}

	public static bool operator !=(Entity<TId> a, Entity<TId> b)
	{
		return !Equals(a, b);
	}
}
