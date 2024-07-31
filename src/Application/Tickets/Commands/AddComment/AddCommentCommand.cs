using MediatR;

namespace Application.Tickets.Commands.AddComment;

public sealed class AddCommentCommand : IRequest
{
	public required Guid TicketId { get; init; }
	public required string Comment {  get; init; }
}
