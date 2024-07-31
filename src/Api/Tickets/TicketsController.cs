using Application.ReadModels.Tickets.Queries.GetById;
using Application.ReadModels.Tickets.Queries.GetMany;
using Application.Tickets.Commands.AddComment;
using Application.Tickets.Commands.Resolve;
using Application.Tickets.Commands.Reject;
using Microsoft.AspNetCore.Mvc;
using Api.Tickets.Requests;
using MediatR;

namespace Api.Tickets;


[ApiController]
[Route("tickets")]
public sealed class TicketsController : ControllerBase
{
	private readonly IMediator mediator;

	public TicketsController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
	{
		var query = new GetManyTicketFullModelQuery() { PageNumber = pageNumber, PageSize = pageSize };
		var result = await mediator.Send(query);

		return Ok(result);
	}

	[HttpGet]
	[Route("{ticketId:guid}")]
	public async Task<IActionResult> GetFullModels([FromRoute] Guid ticketId)
	{
		var query = new GetTicketFullModelByIdQuery() { TicketId = ticketId };
		var result = await mediator.Send(query);

		return Ok(result);
	}

	[HttpPut]
	[Route("{ticketId:guid}/resolve")]
	public async Task<IActionResult> Resolve([FromRoute] Guid ticketId, [FromBody] ResolveTicketRequest request)
	{
		var command = new ResolveTicketCommand()
		{
			TicketId = ticketId,
			Response = request.Response
		};

		await mediator.Send(command);

		return NoContent();
	}

	[HttpPut]
	[Route("{ticketId:guid}/reject")]
	public async Task<IActionResult> Reject([FromRoute] Guid ticketId, [FromBody] RejectTicketRequest request)
	{
		var command = new RejectTicketCommand()
		{
			TicketId = ticketId,
			Response = request.Response
		};

		await mediator.Send(command);

		return NoContent();
	}

	[HttpPut]
	[Route("{ticketId:guid}/comment")]
	public async Task<IActionResult> AddComment([FromRoute] Guid ticketId, [FromBody] AddCommentRequest request)
	{
		var command = new AddCommentCommand()
		{
			TicketId = ticketId,
			Comment = request.Comment
		};

		await mediator.Send(command);

		return NoContent();
	}
}
