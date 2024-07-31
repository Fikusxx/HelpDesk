using Microsoft.Extensions.DependencyInjection;
using Infrastructure.TicketFullModels;
using Marten.Events.Daemon.Resiliency;
using Infrastructure.Common.Options;
using Infrastructure.Interceptors;
using Infrastructure.Accounts;
using Infrastructure.Outbox;
using Consumers.Tickets;
using JasperFx.Core;
using Weasel.Core;
using MassTransit;
using Marten;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
	{
		RegisterRabbitMq(services);
		RegisterMarten(services);

		services.AddTicketsModule();

		services.AddTicketFullModelModule();

		services.AddOutboxModule();

		return services;
	}

	private static IServiceCollection RegisterMarten(this IServiceCollection services)
	{
		services.AddMarten(options =>
		{
			options.UseDefaultSerialization(nonPublicMembersStorage: NonPublicMembersStorage.All,
				enumStorage: EnumStorage.AsString);

			options.Listeners.Add(new OutboxInterceptor());

			options.RetryPolicy(DefaultRetryPolicy.Times(3));

			options.Projections.OnException<Exception>()
						.RetryLater(50.Milliseconds(), 250.Milliseconds(), 500.Milliseconds());
		})
		.UseLightweightSessions()
		.ApplyAllDatabaseChangesOnStartup()
		.AddAsyncDaemon(DaemonMode.HotCold);

		services.ConfigureMarten((sp, options) =>
		{
			var eventStoreOptions = sp.GetRequiredService<EventStoreOptions>();

			options.Connection(eventStoreOptions.ConnectionString);
		});

		return services;
	}

	private static IServiceCollection RegisterRabbitMq(this IServiceCollection services)
	{
		services.AddMassTransit(cfg =>
		{
			cfg.SetKebabCaseEndpointNameFormatter();

			cfg.AddConsumer<TicketSubmittedEventConsumer>();

			cfg.UsingRabbitMq((ctx, options) =>
			{
				var brokerOptions = ctx.GetRequiredService<MessageBrokerOptions>();

				options.Host(new Uri(brokerOptions.Uri), cfg =>
				{
					cfg.Username(brokerOptions.Username);
					cfg.Password(brokerOptions.Password);
				});

				options.ReceiveEndpoint("Admin.Tickets", opt =>
				{
					opt.ConfigureConsumer<TicketSubmittedEventConsumer>(ctx);

					opt.UseMessageRetry(x => x.Interval(5, TimeSpan.FromMilliseconds(100)));
				});

				options.ConfigureEndpoints(ctx);
			});
		});

		return services;
	}
}

