using Domain.Common;

namespace Users.Domain.Tests.Architecture.BusinessRules;

public sealed class BusinessRulesTests
{
	private static readonly Assembly domainAssembly = typeof(IBusinessRule).Assembly;

	[Fact]
	public void BusinessRules_ShouldBeInternal()
	{
		var result = Types.InAssembly(domainAssembly)
			.That()
			.ImplementInterface(typeof(IBusinessRule))
			.Should()
			.NotBePublic()
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void BusinessRules_ShouldBeSealed()
	{
		var result = Types.InAssembly(domainAssembly)
			.That()
			.ImplementInterface(typeof(IBusinessRule))
			.Should()
			.BeSealed()
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void BusinessRules_ShouldEndWithBusinessRule()
	{
		var result = Types.InAssembly(domainAssembly)
			.That()
			.ImplementInterface(typeof(IBusinessRule))
			.Should()
			.HaveNameEndingWith("BusinessRule")
			.GetResult();

		result.FailingTypes.Should().BeNullOrEmpty();
		result.IsSuccessful.Should().BeTrue();
	}
}
