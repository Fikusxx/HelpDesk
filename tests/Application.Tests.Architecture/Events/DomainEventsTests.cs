using HelpDeskAdminContracts.Common;


namespace Users.Domain.Tests.Architecture.Events;

public sealed class DomainEventsTests
{
	private static readonly Assembly eventsAssembly = typeof(IDomainEvent).Assembly;

	[Fact]
	public void DomainEvents_ShouldBeSealed()
	{
		var result = Types.InAssembly(eventsAssembly)
			.That()
			.ImplementInterface(typeof(IDomainEvent))
			.Should()
			.BeSealed()
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void DomainEvents_ShouldEndWithEvent()
	{
		var result = Types.InAssembly(eventsAssembly)
			.That()
			.ImplementInterface(typeof(IDomainEvent))
			.Should()
			.HaveNameEndingWith("event")
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void DomainEvents_ShouldImplementIDomainEvent()
	{
		var result = Types.InAssembly(eventsAssembly)
			.That()
			.AreClasses()
			.Should()
			.ImplementInterface(typeof(IDomainEvent))
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void DomainEvents_ShouldHaveOnePublicConstructorWithParameters()
	{
		var eventTypes = Types.InAssembly(eventsAssembly)
			.That()
			.ImplementInterface(typeof(IDomainEvent))
			.GetTypes()
			.ToList();

		var failingTypes = new List<Type>();

		foreach (var type in eventTypes)
		{
			var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
				.Where(x => x.GetParameters().Length > 0)
				.ToList();

			if (ctors.Count != 1)
				failingTypes.Add(type);
		}

		failingTypes.Should().BeEmpty();
	}

	[Fact]
	public void DomainEvents_ShouldNotHaveAnyDependecies()
	{
		var result = Types.InAssembly(eventsAssembly)
			.Should()
			.NotHaveDependencyOnAny("Application", "Domain", "Infrastructure", "Api")
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void DomainEvents_ShouldHaveInitOrPrivateSettersOnProperties()
	{
		var eventTypes = Types.InAssembly(eventsAssembly)
			.That()
			.ImplementInterface(typeof(IDomainEvent))
			.GetTypes()
			.ToList();

		var failingTypes = new List<Type>();

		foreach (var type in eventTypes)
		{
			var props = type.GetProperties();

			foreach (var prop in props)
			{
				var setMethod = prop.SetMethod;

				if (setMethod is null || setMethod.IsPrivate)
					continue;

				var setMethodReturnParameterModifiers = setMethod.ReturnParameter.GetRequiredCustomModifiers().ToList();
				var hasInitSetter = setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));

				if (hasInitSetter == false)
					failingTypes.Add(type);
			}
		}

		failingTypes.Should().BeEmpty();
	}
}
