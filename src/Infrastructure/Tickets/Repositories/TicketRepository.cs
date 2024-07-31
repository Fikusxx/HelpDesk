using Infrastructure.Common.Repositories;
using Application.Tickets.Contracts;
using Domain.Tickets.ValueObjects;
using Domain.Tickets;
using Marten;

namespace Infrastructure.Accounts.Repositories;

internal class TicketRepository : AggregateRootRepository<Ticket, TicketId>, ITicketRepository
{
	public TicketRepository(IDocumentSession ctx) : base(ctx)
	{ }
}
