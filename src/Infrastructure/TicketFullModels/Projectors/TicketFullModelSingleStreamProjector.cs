using Application.ReadModels.Tickets;
using HelpDeskAdminContracts.Tickets;
using Marten.Events.Aggregation;
using Domain.Tickets.Enums;

namespace Infrastructure.TicketFullModels.Projectors;

public sealed class TicketFullModelSingleStreamProjector : SingleStreamProjection<TicketFullModel>
{
    public TicketFullModelSingleStreamProjector()
    {
		IncludeType<TicketCreatedEvent>();
		IncludeType<TicketCommentAddedEvent>();
		IncludeType<TicketResolvedEvent>();
		IncludeType<TicketRejectedEvent>();
	}

	public TicketFullModel Create(TicketCreatedEvent e)
	{
		var readModel = new TicketFullModel(e.TicketId, null, Status.PENDING, new());

		return readModel;
	}

	public void Apply(TicketFullModel snapshot, TicketCommentAddedEvent e)
	{
		snapshot.Comments.Add(new CommentModel(e.Comment, e.CreatedAt));
	}

	public void Apply(TicketFullModel snapshot, TicketResolvedEvent e)
	{
		snapshot.Response = e.Response;
		snapshot.Status = Status.RESOLVED;
	}

	public void Apply(TicketFullModel snapshot, TicketRejectedEvent e)
	{
		snapshot.Response = e.Response;
		snapshot.Status = Status.REJECTED;
	}
}
