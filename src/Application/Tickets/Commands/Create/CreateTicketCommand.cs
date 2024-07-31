using MediatR;

namespace Application.Tickets.Commands.Create;

public sealed class CreateTicketCommand : IRequest
{
	public required Guid TicketId { get; init; }
}
