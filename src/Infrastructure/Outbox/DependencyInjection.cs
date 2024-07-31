using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Outbox.Registries;
using Infrastructure.Outbox.Services;
using Marten;

namespace Infrastructure.Outbox;

internal static class DependencyInjection
{
	internal static IServiceCollection AddOutboxModule(this IServiceCollection services)
	{
		services.AddScoped<OutboxMessagePublisher>();

		services.ConfigureMarten(options =>
		{
			options.RegisterDocumentType<OutboxMessage>();

			options.Schema.Include<OutboxMessageRegistry>();
		});

		return services;
	}
}
