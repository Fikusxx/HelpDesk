using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Common.Options;

public sealed class EventStoreOptions
{
	[Required]
	public required string ConnectionString { get; init; }
}
