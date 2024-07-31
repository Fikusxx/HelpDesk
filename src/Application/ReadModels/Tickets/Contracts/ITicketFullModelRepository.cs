using System.Linq.Expressions;
using Application.Common;
using Marten.Pagination;

namespace Application.ReadModels.Tickets.Contracts;

public interface ITicketFullModelRepository : IReadModelRepository<TicketFullModel>
{
	public Task<IPagedList<TicketFullModel>> GetAllAsync(Expression<Func<TicketFullModel, bool>> predicate,
		int pageNumber = 1, int pageSize = 10);
}
