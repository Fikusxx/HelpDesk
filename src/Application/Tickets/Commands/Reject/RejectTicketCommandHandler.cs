using Application.Common.Exceptions;
using Application.Tickets.Contracts;
using Domain.Tickets.ValueObjects;
using Domain.Tickets;
using MediatR;

namespace Application.Tickets.Commands.Reject;

internal sealed class RejectTicketCommandHandler : IRequestHandler<RejectTicketCommand>
{
	private readonly ITicketRepository ctx;

	public RejectTicketCommandHandler(ITicketRepository ctx)
	{
		this.ctx = ctx;
	}

	public async Task Handle(RejectTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = await ctx.LoadAsync(request.TicketId, cancellationToken);

		if (ticket is null)
			throw new NotFoundException($"{nameof(Ticket)} not found");

		ticket.Reject(Response.CreateNew(request.Response));

		await ctx.StoreAsync(ticket, cancellationToken);
	}
}
