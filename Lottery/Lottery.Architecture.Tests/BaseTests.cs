using NetArchTest.Rules;
using System.Reflection;

namespace Lottery.Architecture.Tests
{
    public class BaseTests
    {
        protected readonly Types AllTypes;
        protected readonly Assembly LotteryCoreAssembly;
        protected readonly Assembly LotteryServicesAssembly;

        public BaseTests(ProjectTypesFixture fixtures)
        {
            ArgumentNullException.ThrowIfNull(fixtures);
            AllTypes = fixtures.AllTypes;
            LotteryCoreAssembly = ProjectTypesFixture.LotteryCoreAssembly;
            LotteryServicesAssembly = ProjectTypesFixture.LotteryServicesAssembly;
        }
    }
}
