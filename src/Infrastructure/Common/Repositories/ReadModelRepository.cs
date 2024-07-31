using Application.Common;
using Marten;

namespace Infrastructure.Repositories;

internal abstract class ReadModelRepository<T> : IReadModelRepository<T> where T : class, IReadModel
{
	protected readonly IQuerySession ctx;

	public ReadModelRepository(IQuerySession ctx)
	{
		this.ctx = ctx;
	}

	public async Task<T?> LoadAsync(Guid id, CancellationToken ct = default)
	{
		var snapshot = await ctx.LoadAsync<T>(id, ct);

		return snapshot;
	}
}
