

namespace Application.Common;

public interface IReadModelRepository<T> where T : IReadModel
{
	public Task<T?> LoadAsync(Guid id, CancellationToken ct = default);
}
