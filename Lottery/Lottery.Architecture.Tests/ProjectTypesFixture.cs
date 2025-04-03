using Lottery.Core.Helpers;
using NetArchTest.Rules;
using System.Reflection;

namespace Lottery.Architecture.Tests
{
    public class ProjectTypesFixture
    {
        public Types AllTypes;
        public static Assembly LotteryCoreAssembly = typeof(LotteryHelper).Assembly;
        public static Assembly LotteryServicesAssembly = typeof(LotteryManagerService).Assembly;

        public ProjectTypesFixture()
        {
            AllTypes = Types.InAssemblies(new List<Assembly> {
                LotteryCoreAssembly,
                LotteryServicesAssembly
            });
        }
    }
}
