using Application.Tickets.Commands.Create;
using HelpDeskUserContracts.Tickets;
using MassTransit;
using MediatR;

namespace Consumers.Tickets;

public sealed class TicketSubmittedEventConsumer : IConsumer<TicketSubmittedEvent>
{
	private readonly IMediator mediator;

	public TicketSubmittedEventConsumer(IMediator mediator)
	{
		this.mediator = mediator;
	}

	public async Task Consume(ConsumeContext<TicketSubmittedEvent> context)
	{
		var command = new CreateTicketCommand() { TicketId = context.Message.TicketId };
		await mediator.Send(command);
	}
}