using Application.ReadModels.Tickets.Contracts;
using Application.Common.Exceptions;
using Application.Common;
using MediatR;

namespace Application.ReadModels.Tickets.Queries.GetMany;

internal sealed class GetManyTicketFullModelQueryHandler : IRequestHandler<GetManyTicketFullModelQuery, PagedData<TicketFullModel>>
{
	private readonly ITicketFullModelRepository ctx;

	public GetManyTicketFullModelQueryHandler(ITicketFullModelRepository ctx)
	{
		this.ctx = ctx;
	}

	public async Task<PagedData<TicketFullModel>> Handle(GetManyTicketFullModelQuery request, CancellationToken cancellationToken)
	{
		if (request.PageNumber < 1 || request.PageSize < 1)
			throw new PaginationArgumentsException("Invalid pagination parameters");

		var tickets = await ctx.GetAllAsync(x => true, pageNumber: request.PageNumber, pageSize: request.PageSize);

		var pagedData = PagedData<TicketFullModel>.Create(tickets);

		return pagedData;
	}
}
