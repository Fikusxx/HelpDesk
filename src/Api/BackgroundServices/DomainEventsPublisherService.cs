using Infrastructure.Outbox.Services;

namespace UI.BackgroundServices;

public sealed class DomainEventsPublisherService : BackgroundService
{
	private readonly IServiceProvider provider;

	public DomainEventsPublisherService(IServiceProvider provider)
	{
		this.provider = provider;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		using var scope = provider.CreateScope();
		var publishService = scope.ServiceProvider.GetRequiredService<OutboxMessagePublisher>();

		while (stoppingToken.IsCancellationRequested == false)
		{
			await Task.Delay(5000, stoppingToken);
			await publishService.PublishOutboxMessages();
		}
	}
}
