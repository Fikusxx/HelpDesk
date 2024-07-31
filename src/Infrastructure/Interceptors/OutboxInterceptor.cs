using Infrastructure.Outbox;
using HelpDeskAdminContracts.Common;
using Newtonsoft.Json;
using Marten;

namespace Infrastructure.Interceptors;

internal sealed class OutboxInterceptor : DocumentSessionListenerBase
{
	// TODO
	public override void BeforeSaveChanges(IDocumentSession session)
	{
		base.BeforeSaveChanges(session);
	}

	public override Task BeforeSaveChangesAsync(IDocumentSession session, CancellationToken token)
	{
		var pending = session.PendingChanges;

		if (pending is null)
			return Task.CompletedTask;

		var events = pending.Streams().SelectMany(x => x.Events).ToList();

		if (events.Count <= 0)
			return Task.CompletedTask;

		var outboxMessages = new List<OutboxMessage>();
		var options = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

		events.ForEach(x =>
		{
			if (x.Data is IDomainEvent @event)
			{
				var data = JsonConvert.SerializeObject(@event, options);
				var outbox = new OutboxMessage()
				{
					Id = Guid.NewGuid(),
					OccuredOn = DateTimeOffset.UtcNow,
					ExecutedBy = "", // TODO (token)
					Event = data
				};

				outboxMessages.Add(outbox);
			}
		});

		if (outboxMessages.Count <= 0)
			return Task.CompletedTask;

		session.Insert(outboxMessages.AsEnumerable());

		return Task.CompletedTask;
	}
}
