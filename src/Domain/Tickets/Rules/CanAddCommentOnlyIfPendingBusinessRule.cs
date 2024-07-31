using Domain.Tickets.Enums;
using Domain.Common;


namespace Domain.Tickets.Rules;

internal sealed class CanAddCommentOnlyIfPendingBusinessRule : IBusinessRule
{
	public string Message => $"{nameof(Ticket)} is not {Status.PENDING}";
	private readonly Status currentStatus;

	public CanAddCommentOnlyIfPendingBusinessRule(Status currentStatus)
	{
		this.currentStatus = currentStatus;
	}

	public bool IsBroken()
	{
		if (currentStatus != Status.PENDING)
			return true;

		return false;
	}
}
