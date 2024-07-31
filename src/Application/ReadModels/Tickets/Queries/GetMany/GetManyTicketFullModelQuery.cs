using Application.Common;
using MediatR;

namespace Application.ReadModels.Tickets.Queries.GetMany;

public sealed class GetManyTicketFullModelQuery : IRequest<PagedData<TicketFullModel>>
{
	public int PageNumber { get; init; } = 1;
	public int PageSize { get; init; } = 10;
}
