using Application.Common.Contracts.Persistense;
using Domain.Tickets.ValueObjects;
using Domain.Tickets;

namespace Application.Tickets.Contracts;

public interface ITicketRepository : IAggregateRootRepository<Ticket, TicketId>
{ }
