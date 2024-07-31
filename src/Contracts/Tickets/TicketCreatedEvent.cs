using HelpDeskAdminContracts.Common;

namespace HelpDeskAdminContracts.Tickets;
 
public sealed record TicketCreatedEvent : IDomainEvent
{
	public Guid TicketId { get; init; }

	public TicketCreatedEvent(Guid ticketId)
	{
		TicketId = ticketId;
	}
}
