using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Common.Options;

public sealed class MessageBrokerOptions
{
	[Required]
	public required string Uri { get; init; }

	[Required]
	public required string Username { get; init; }

	[Required]
	public required string Password { get; init; }
}
