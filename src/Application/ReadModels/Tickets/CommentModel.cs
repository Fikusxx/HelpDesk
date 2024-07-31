

namespace Application.ReadModels.Tickets;

public sealed class CommentModel
{
	public string Comment { get; set; }
	public DateTimeOffset CreatedAt { get; set; }

	public CommentModel(string comment, DateTimeOffset createdAt)
	{
		Comment = comment;
		CreatedAt = createdAt;
	}
}
