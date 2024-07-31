using Application.ReadModels.Tickets.Contracts;
using Application.Common.Exceptions;
using MediatR;

namespace Application.ReadModels.Tickets.Queries.GetById;

internal sealed class GetTicketFullModelByIdQueryHandler : IRequestHandler<GetTicketFullModelByIdQuery, TicketFullModel>
{
	private readonly ITicketFullModelRepository ctx;

	public GetTicketFullModelByIdQueryHandler(ITicketFullModelRepository ctx)
	{
		this.ctx = ctx;
	}

	public async Task<TicketFullModel> Handle(GetTicketFullModelByIdQuery request, CancellationToken cancellationToken)
	{
		var ticket = await ctx.LoadAsync(request.TicketId, cancellationToken);

		if (ticket is null)
			throw new NotFoundException($"{nameof(TicketFullModel)} not found");

		return ticket;
	}
}
