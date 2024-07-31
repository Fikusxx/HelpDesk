using HelpDeskAdminContracts.Common;

namespace HelpDeskAdminContracts.Tickets;

public sealed record class TicketResolvedEvent : IDomainEvent
{
	public Guid TicketId { get; init; }
	public string Response { get; init; }

	public TicketResolvedEvent(Guid ticketId, string response)
	{
		TicketId = ticketId;
		Response = response;
	}
}
