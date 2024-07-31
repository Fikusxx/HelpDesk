using Application.Common.Exceptions;
using Application.Tickets.Contracts;
using Domain.Tickets.ValueObjects;
using Domain.Tickets;
using MediatR;

namespace Application.Tickets.Commands.AddComment;

internal sealed class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
{
	private readonly ITicketRepository ctx;
	private readonly TimeProvider timeProvider;

	public AddCommentCommandHandler(ITicketRepository ctx, TimeProvider timeProvider)
	{
		this.ctx = ctx;
		this.timeProvider = timeProvider;
	}

	public async Task Handle(AddCommentCommand request, CancellationToken cancellationToken)
	{
		var ticket = await ctx.LoadAsync(request.TicketId, cancellationToken);

		if (ticket is null)
			throw new NotFoundException($"{nameof(Ticket)} not found");

		ticket.AddComment(Comment.CreateNew(request.Comment, timeProvider.GetUtcNow()));

		await ctx.StoreAsync(ticket, cancellationToken);
	}
}
