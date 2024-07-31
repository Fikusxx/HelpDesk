using Application.ReadModels.Tickets.Contracts;
using Application.ReadModels.Tickets;
using Infrastructure.Repositories;
using System.Linq.Expressions;
using Marten.Pagination;
using Marten;

namespace Infrastructure.TicketFullModels.Repositories;

internal sealed class TicketFullModelRepository 
	: ReadModelRepository<TicketFullModel>, ITicketFullModelRepository
{
	public TicketFullModelRepository(IQuerySession ctx) : base(ctx)
	{ }

	public async Task<IPagedList<TicketFullModel>> GetAllAsync(
		Expression<Func<TicketFullModel, bool>> predicate,
		int pageNumber = 1, int pageSize = 10)
	{
		var query = ctx.Query<TicketFullModel>().AsQueryable();

		if (predicate is not null)
			query = query.Where(predicate);

		var list = await query.ToPagedListAsync(pageNumber, pageSize);

		return list;
	}
}
