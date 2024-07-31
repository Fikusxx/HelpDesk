using Infrastructure.Common.Options;
using Microsoft.Extensions.Options;

namespace Api;

public static class DependencyInjection
{
	public static IServiceCollection AddEventStoreOptions(this IServiceCollection services)
	{
		services.AddOptions<EventStoreOptions>()
			.BindConfiguration(nameof(EventStoreOptions))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		services.AddSingleton(sp =>
		{
			return sp.GetRequiredService<IOptions<EventStoreOptions>>().Value;
		});

		return services;
	}

	public static IServiceCollection AddMessageBrokerOptions(this IServiceCollection services)
	{
		services.AddOptions<MessageBrokerOptions>()
			.BindConfiguration(nameof(MessageBrokerOptions))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		services.AddSingleton(sp =>
		{
			return sp.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;
		});

		return services;
	}
}
