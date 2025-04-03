using Xunit;

namespace Lottery.Architecture.Tests
{
    [Collection("Project Types")]
    public class NamingConventionTests : BaseTests
    {
        public NamingConventionTests(ProjectTypesFixture fixtures)
            : base(fixtures)
        {
        }

        [Fact]
        public void AllServiceClassNamesShouldEndWithService()
        {
            AllTypes.That().ResideInNamespaceMatching("Lottery.Services")
                .Should().HaveNameEndingWith("Service")
                .AssertIsSuccessful();
        }
    }
}
