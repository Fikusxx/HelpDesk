using Application.ReadModels.Tickets;
using Marten;

namespace Infrastructure.TicketFullModels.Registries;

internal sealed class TicketFullModelRegistry : MartenRegistry
{
    public TicketFullModelRegistry()
    {
        For<TicketFullModel>().Identity(x => x.Id);
    }
}
