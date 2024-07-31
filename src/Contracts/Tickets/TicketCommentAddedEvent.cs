using HelpDeskAdminContracts.Common;

namespace HelpDeskAdminContracts.Tickets;

public sealed record TicketCommentAddedEvent : IDomainEvent
{
	public Guid TicketId { get; init; }
	public string Comment {  get; init; }
	public DateTimeOffset CreatedAt { get; init; }

	public TicketCommentAddedEvent(Guid ticketId, string comment, DateTimeOffset createdAt)
	{
		TicketId = ticketId;
		Comment = comment;
		CreatedAt = createdAt;
	}
}
