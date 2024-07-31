using HelpDeskAdminContracts.Common;

namespace HelpDeskAdminContracts.Tickets;

public sealed record TicketRejectedEvent : IDomainEvent
{
	public Guid TicketId { get; init; }
	public string Response { get; init; }

	public TicketRejectedEvent(Guid ticketId, string response)
	{
		TicketId = ticketId;
		Response = response;
	}
}
