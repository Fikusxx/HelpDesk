using Infrastructure.TicketFullModels.Repositories;
using Infrastructure.TicketFullModels.Projectors;
using Infrastructure.TicketFullModels.Registries;
using Microsoft.Extensions.DependencyInjection;
using Application.ReadModels.Tickets.Contracts;
using Application.ReadModels.Tickets;
using Marten.Events.Projections;
using HelpDeskAdminContracts.Tickets;
using Marten;

namespace Infrastructure.TicketFullModels;

internal static class DependencyInjection
{
	internal static IServiceCollection AddTicketFullModelModule(this IServiceCollection services)
	{
		services.AddScoped<ITicketFullModelRepository, TicketFullModelRepository>();

		services.ConfigureMarten(options =>
		{
			options.RegisterDocumentType<TicketFullModel>();

			options.Events.AddEventType<TicketCreatedEvent>();
			options.Events.AddEventType<TicketCommentAddedEvent>();
			options.Events.AddEventType<TicketResolvedEvent>();
			options.Events.AddEventType<TicketRejectedEvent>();

			options.Schema.Include<TicketFullModelRegistry>();

			options.Projections.Add<TicketFullModelSingleStreamProjector>(lifecycle: ProjectionLifecycle.Inline);
		});

		return services;
	}
}
