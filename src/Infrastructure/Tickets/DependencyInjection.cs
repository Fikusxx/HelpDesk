using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Accounts.Repositories;
using Infrastructure.Accounts.Registries;
using Infrastructure.Tickets.Projectors;
using Application.Tickets.Contracts;
using Marten.Events.Projections;
using HelpDeskAdminContracts.Tickets;
using Domain.Tickets;
using Marten;

namespace Infrastructure.Accounts;

internal static class DependencyInjection
{
	internal static IServiceCollection AddTicketsModule(this IServiceCollection services)
	{
		services.AddScoped<ITicketRepository, TicketRepository>();

		services.ConfigureMarten(options =>
		{
			options.RegisterDocumentType<Ticket>();

			options.Events.AddEventType<TicketCreatedEvent>();
			options.Events.AddEventType<TicketCommentAddedEvent>();
			options.Events.AddEventType<TicketResolvedEvent>();
			options.Events.AddEventType<TicketRejectedEvent>();

			options.Schema.Include<TicketRegistry>();

			options.Projections.Add<TicketSingleStreamProjector>(lifecycle: ProjectionLifecycle.Inline);
		});

		return services;
	}
}
