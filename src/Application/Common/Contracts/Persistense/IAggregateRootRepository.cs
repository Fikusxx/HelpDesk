using Domain.Common;

namespace Application.Common.Contracts.Persistense;

public interface IAggregateRootRepository<TAggregateRoot, TId>
    where TAggregateRoot : IAggregateRoot<TId>
    where TId : IHasId
{
    public Task StoreAsync(TAggregateRoot aggregate, CancellationToken ct = default);
    public Task<TAggregateRoot?> AggregateAsync(Guid id, int? version = null, CancellationToken ct = default);
    public Task<TAggregateRoot?> LoadAsync(Guid id, CancellationToken ct = default);
}