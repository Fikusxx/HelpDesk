using Application.Common.Exceptions;
using System.Net.Mime;
using Newtonsoft.Json;
using UsersAPI.Models;
using Domain.Common;
using System.Net;

namespace UsersAPI.Middlewares;

public sealed class ExceptionMiddleware
{
	private readonly RequestDelegate next;

	public ExceptionMiddleware(RequestDelegate next)
	{
		this.next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{

			await HandleExceptionAsync(context, ex);
		}
	}

	private Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = MediaTypeNames.Application.Json;
		HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

		var errorDetails = new ExceptionDetails()
		{
			ErrorMessage = exception.Message,
			ErrorType = exception.GetType().Name,
			TraceId = context.TraceIdentifier
		};

		string result = JsonConvert.SerializeObject(errorDetails);

		switch (exception)
		{
			case NotFoundException _:
				statusCode = HttpStatusCode.NotFound;
				break;

			case PaginationArgumentsException _:
				statusCode = HttpStatusCode.BadRequest;
				break;

			case BusinessRuleValidationException _:
				statusCode = HttpStatusCode.BadRequest;
				break;

			default:
				break;
		}

		context.Response.StatusCode = (int)statusCode;

		return context.Response.WriteAsync(result);
	}
}
