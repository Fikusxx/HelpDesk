using Domain.Tickets.Enums;
using Application.Common;

namespace Application.ReadModels.Tickets;

public sealed class TicketFullModel : IReadModel
{
	public Guid Id { get; set; }
	public string? Response { get; set; }
	public Status Status { get; set; }
	public List<CommentModel> Comments { get; set; } = new();

	public TicketFullModel(Guid id, string? response, Status status, List<CommentModel> comments)
	{
		Id = id;
		Response = response;
		Status = status;
		Comments = comments;
	}
}
