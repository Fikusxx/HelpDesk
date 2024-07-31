using Application.Tickets.Contracts;
using Domain.Tickets.ValueObjects;
using Domain.Tickets;
using MediatR;

namespace Application.Tickets.Commands.Create;

internal sealed class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
{
	private readonly ITicketRepository ctx;

	public CreateTicketCommandHandler(ITicketRepository ticketCtx)
	{
		this.ctx = ticketCtx;
	}

	public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = Ticket.CreateNew(TicketId.CreateNew(request.TicketId));

		await ctx.StoreAsync(ticket, cancellationToken);
	}
}
