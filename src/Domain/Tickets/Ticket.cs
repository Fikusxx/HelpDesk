using System.Text.Json.Serialization;
using Domain.Tickets.ValueObjects;
using HelpDeskAdminContracts.Tickets;
using Domain.Tickets.Enums;
using Domain.Tickets.Rules;
using Domain.Common;

namespace Domain.Tickets;

public sealed class Ticket : AggregateRoot<TicketId>
{
	public Response? Response { get; private set; }
	public Status Status { get; private set; }
	public IReadOnlyList<Comment> Comments => comments.AsReadOnly();
	private readonly List<Comment> comments = new();

	private Ticket()
	{ }

	private Ticket(TicketId ticketId)
	{
		var @event = new TicketCreatedEvent(ticketId.Value);

		Apply(@event);
		AddDomainEvent(@event);
	}

	[JsonConstructor]
	internal Ticket(TicketId id, Response? response, Status status, List<Comment> comments)
	{
		this.Id = id;
		this.Response = response;
		this.Status = status;
		this.comments = comments;
	}

	public static Ticket CreateNew(TicketId ticketId)
	{
		return new Ticket(ticketId);
	}

	public void Resolve(Response response)
	{
		CheckRule(new TicketShouldNotBeCompletedBusinessRule(Status));

		var @event = new TicketResolvedEvent(this.Id.Value, response.Value);

		Apply(@event);
		AddDomainEvent(@event);
	}

	public void Reject(Response response)
	{
		CheckRule(new TicketShouldNotBeCompletedBusinessRule(Status));

		var @event = new TicketRejectedEvent(this.Id.Value, response.Value);

		Apply(@event);
		AddDomainEvent(@event);
	}

	public void AddComment(Comment comment)
	{
		CheckRule(new CanAddCommentOnlyIfPendingBusinessRule(Status));

		var @event = new TicketCommentAddedEvent(this.Id.Value, comment.Value, comment.CreatedAt);

		Apply(@event);
		AddDomainEvent(@event);
	}


	#region Events

	private void Apply(TicketCreatedEvent e)
	{
		this.Id = TicketId.CreateNew(e.TicketId);
		this.Status = Status.PENDING;
	}

	private void Apply(TicketResolvedEvent e)
	{
		this.Response = Response.CreateNew(e.Response);
		this.Status = Status.RESOLVED;
	}

	private void Apply(TicketRejectedEvent e)
	{
		this.Response = Response.CreateNew(e.Response);
		this.Status = Status.REJECTED;
	}

	private void Apply(TicketCommentAddedEvent e)
	{
		var comment = Comment.CreateNew(e.Comment, e.CreatedAt);
		this.comments.Add(comment);
	}

	#endregion
}
