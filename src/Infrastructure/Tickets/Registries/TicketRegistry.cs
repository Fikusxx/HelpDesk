using Domain.Tickets;
using Marten;

namespace Infrastructure.Accounts.Registries;

internal sealed class TicketRegistry : MartenRegistry
{
    public TicketRegistry()
    {
        For<Ticket>().Identity(x => x.EntityId);
    }
}
