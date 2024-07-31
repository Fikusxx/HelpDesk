using HelpDeskAdminContracts.Tickets;
using HelpDeskAdminContracts.Common;
using Newtonsoft.Json;
using MassTransit;
using Marten;

namespace Infrastructure.Outbox.Services;

public sealed class OutboxMessagePublisher
{
    private readonly IDocumentSession session;
    private readonly IPublishEndpoint publishEndpoint;
    private const string occuredOn = "OccuredOn";
    private const string executedBy = "ExecutedBy";

    public OutboxMessagePublisher(IDocumentSession session, IPublishEndpoint publishEndpoint)
    {
        this.session = session;
        this.publishEndpoint = publishEndpoint;
    }

    public async Task PublishOutboxMessages()
    {
        var events = await session.Query<OutboxMessage>()
            .OrderBy(x => x.OccuredOn)
            .Take(10)
            .ToListAsync();

        if (events.Count <= 0)
            return;

        var options = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        foreach (var e in events)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(e.Event, options);

            switch (domainEvent)
            {
                case TicketRejectedEvent @event:
                    await publishEndpoint.Publish(@event, opt =>
                    {
                        opt.MessageId = e.Id;
                        opt.Headers.Set(occuredOn, e.OccuredOn.ToString());
                        opt.Headers.Set(executedBy, e.ExecutedBy);
                    });
                    break;

                case TicketResolvedEvent @event:
                    await publishEndpoint.Publish(@event, opt =>
                    {
                        opt.MessageId = e.Id;
                        opt.Headers.Set(occuredOn, e.OccuredOn.ToString());
                        opt.Headers.Set(executedBy, e.ExecutedBy);
                    });
                    break;
            }
        }

        session.DeleteObjects(events);
        await session.SaveChangesAsync();
    }
}
