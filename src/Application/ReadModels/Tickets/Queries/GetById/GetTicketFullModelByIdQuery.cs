using MediatR;

namespace Application.ReadModels.Tickets.Queries.GetById;

public sealed class GetTicketFullModelByIdQuery : IRequest<TicketFullModel>
{
	public required Guid TicketId { get; init; }
}
