using Domain.Tickets.Enums;
using Domain.Common;

namespace Domain.Tickets.Rules;

internal sealed class TicketShouldNotBeCompletedBusinessRule : IBusinessRule
{
	public string Message => $"{nameof(Ticket)} has already been {Status.RESOLVED} or {Status.REJECTED}";
	private readonly Status currentStatus;

	public TicketShouldNotBeCompletedBusinessRule(Status currentStatus)
	{
		this.currentStatus = currentStatus;
	}

	public bool IsBroken()
	{
		if (currentStatus == Status.RESOLVED || currentStatus == Status.REJECTED)
			return true;

		return false;
	}
}
