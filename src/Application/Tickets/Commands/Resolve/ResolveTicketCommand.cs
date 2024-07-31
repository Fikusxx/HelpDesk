using MediatR;

namespace Application.Tickets.Commands.Resolve;

public sealed class ResolveTicketCommand : IRequest
{
	public required Guid TicketId { get; init; }
	public required string Response {  get; init; }
}
