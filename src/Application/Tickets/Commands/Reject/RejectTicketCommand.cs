using MediatR;

namespace Application.Tickets.Commands.Reject;

public sealed class RejectTicketCommand : IRequest
{
	public required Guid TicketId { get; init; }
	public required string Response { get; init; }
}
