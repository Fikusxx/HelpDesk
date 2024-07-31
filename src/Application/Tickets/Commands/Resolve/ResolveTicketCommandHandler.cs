using Application.Common.Exceptions;
using Application.Tickets.Contracts;
using Domain.Tickets.ValueObjects;
using Domain.Tickets;
using MediatR;

namespace Application.Tickets.Commands.Resolve;

internal sealed class ResolveTicketCommandHandler : IRequestHandler<ResolveTicketCommand>
{
	private readonly ITicketRepository ctx;

	public ResolveTicketCommandHandler(ITicketRepository ticketCtx)
	{
		this.ctx = ticketCtx;
	}

	public async Task Handle(ResolveTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = await ctx.LoadAsync(request.TicketId, cancellationToken);

		if(ticket is null)
			throw new NotFoundException($"{nameof(Ticket)} not found");

		ticket.Resolve(Response.CreateNew(request.Response));

		await ctx.StoreAsync(ticket, cancellationToken);
	}
}
