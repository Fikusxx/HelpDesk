using Domain.Common;

namespace Users.Domain.Tests.Architecture.ValueObjects;

public sealed class ValueObjectTests
{
	private static readonly Assembly domainAssembly = typeof(IEntity<>).Assembly;

	[Fact]
	public void ValueObjects_ShouldBeRecords()
	{
		var valueObjects = GetValueObjectTypes();

		var failingTypes = new List<Type>();

		foreach (var vo in valueObjects)
		{
			var isRecord = vo.GetMethods().Any(x => x.Name == "<Clone>$");

			if (isRecord == false)
				failingTypes.Add(vo);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void ValueObjects_ShouldBeSealed()
	{
		var valueObjects = GetValueObjectTypes();

		var failingTypes = new List<Type>();

		foreach (var vo in valueObjects)
		{
			if (vo.IsSealed == false)
				failingTypes.Add(vo);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void ValueObjects_ShouldNotHavePublicConstructor()
	{
		var valueObjects = GetValueObjectTypes();

		var failingTypes = new List<Type>();

		foreach (var vo in valueObjects)
		{
			var hasPublicConstructor = vo.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length > 0;

			if (hasPublicConstructor)
				failingTypes.Add(vo);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void ValueObjects_ShouldNotHavePublicSetters()
	{
		var valueObjects = GetValueObjectTypes();

		var failingTypes = new List<Type>();

		foreach (var vo in valueObjects)
		{
			var hasPublicSetter = vo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Any(x => x.GetSetMethod()?.IsPublic == true);

			if (hasPublicSetter)
				failingTypes.Add(vo);
		}

		failingTypes.Should().BeEmpty();
	}

	private static List<Type> GetValueObjectTypes()
	{
		var valueObjects = Types.InAssembly(domainAssembly)
			.That()
			.AreClasses()
			.And()
			.DoNotInherit(typeof(AggregateRoot<>))
			.GetTypes()
			.Where(x => x.GetMethods().Any(x => x.Name == "CreateNew"))
			.ToList();

		return valueObjects;
	}
}
