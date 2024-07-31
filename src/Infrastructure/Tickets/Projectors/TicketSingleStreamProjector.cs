using Marten.Events.Aggregation;
using HelpDeskAdminContracts.Tickets;
using Domain.Tickets;

namespace Infrastructure.Tickets.Projectors;

public sealed class TicketSingleStreamProjector : SingleStreamProjection<Ticket>
{
    public TicketSingleStreamProjector()
    {
        IncludeType<TicketCreatedEvent>();
        IncludeType<TicketCommentAddedEvent>();
        IncludeType<TicketResolvedEvent>();
        IncludeType<TicketRejectedEvent>();
    }
}
