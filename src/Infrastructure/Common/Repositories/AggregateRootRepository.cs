using Application.Common.Contracts.Persistense;
using Domain.Common;
using Marten;

namespace Infrastructure.Common.Repositories;

internal abstract class AggregateRootRepository<T, TId> : IAggregateRootRepository<T, TId>
	where T : class, IAggregateRoot<TId>
	where TId : class, IHasId
{
	protected readonly IDocumentSession ctx;

	public AggregateRootRepository(IDocumentSession ctx)
	{
		this.ctx = ctx;
	}

	public Task<T?> AggregateAsync(Guid id, int? version = null, CancellationToken ct = default)
	{
		throw new NotImplementedException();
	}

	public async Task<T?> LoadAsync(Guid id, CancellationToken ct = default)
	{
		var snapshot = await ctx.LoadAsync<T>(id, ct);

		return snapshot;
	}

	public async Task StoreAsync(T aggregate, CancellationToken ct = default)
	{
		var events = aggregate.DomainEvents;
		var id = aggregate.Id.Value;
		ctx.Events.Append(id, events);
		await ctx.SaveChangesAsync(ct);
	}
}