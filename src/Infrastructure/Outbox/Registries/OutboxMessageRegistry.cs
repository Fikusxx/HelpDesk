using Marten;

namespace Infrastructure.Outbox.Registries;

internal sealed class OutboxMessageRegistry : MartenRegistry
{
	public OutboxMessageRegistry() 
	{
		For<OutboxMessage>().Identity(x => x.Id);
		For<OutboxMessage>().Index(x => x.OccuredOn);
	}

}
