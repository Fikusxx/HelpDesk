using System.Text.Json.Serialization;
using Domain.Common;

namespace Users.Domain.Tests.Architecture.Entities;

public sealed class EntitiesTests
{
	private static readonly Assembly domainAssembly = typeof(IEntity<>).Assembly;
	private static readonly List<Type> arTypes = Types.InAssembly(domainAssembly)
			.That()
			.Inherit(typeof(AggregateRoot<>))
			.GetTypes()
			.ToList();

	[Fact]
	public void Entities_ShouldHaveInternalConstructorMarkedWithJsonConstructorAttribute()
	{
		var failingTypes = new List<Type>();

		foreach (var type in arTypes)
		{
			var ctors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
			var hasJsonCtorAttribute = ctors.Any(x => x.GetCustomAttribute(typeof(JsonConstructorAttribute)) is not null);

			if (hasJsonCtorAttribute == false)
				failingTypes.Add(type);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void Entities_ShouldNotHavePublicConstructor()
	{
		var failingTypes = new List<Type>();

		foreach (var type in arTypes)
		{
			var hasPublicConstructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length > 0;

			if (hasPublicConstructor)
				failingTypes.Add(type);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void Entities_ShouldNotHavePublicSetters()
	{
		var failingTypes = new List<Type>();

		foreach (var type in arTypes)
		{
			var hasPublicSetter = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Any(x => x.GetSetMethod()?.IsPublic == true);

			if (hasPublicSetter)
				failingTypes.Add(type);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void Entities_ShouldBeSealed()
	{
		var result = Types.InAssembly(domainAssembly)
			.That()
			.Inherit(typeof(AggregateRoot<>))
			.Should()
			.BeSealed()
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Entities_ShouldNotHaveAnyDependecies()
	{
		var result = Types.InAssembly(domainAssembly)
			.Should()
			.NotHaveDependencyOnAny("Application", "Infrastructure", "Api")
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Entities_ShouldHaveOneParameterelessConstructor()
	{
		var failingTypes = new List<Type>();

		foreach (var type in arTypes)
		{
			var privateCtorCount = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(x => x.IsPrivate && x.GetParameters().Length == 0)
				.Count();

			if (privateCtorCount != 1)
				failingTypes.Add(type);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void Entities_ShouldImpelementPrivateApplyMethods()
	{
		var failingTypes = new List<Type>();
		var applyMethodName = "Apply";

		foreach (var type in arTypes)
		{
			var count = type.Methods().Where(x => x.IsPrivate && x.Name == applyMethodName).Count();

			if (count == 0)
				failingTypes.Add(type);
		}

		failingTypes.Should().BeEmpty();
	}
}
