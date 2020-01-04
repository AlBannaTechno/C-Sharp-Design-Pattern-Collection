using System.Collections.Generic;
using Autofac;
using MainProject.CreationalPatterns.Singleton.S_1_SingletonImplementation;
using MainProject.CreationalPatterns.Singleton.S_3_Singleton_TestabilityWithInterfaceSolution;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class SingletonTestabilityWithDiSolutionTest
    {
        [Test]
        public void Test()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<DummyDatabase>().As<IDatabase>().SingleInstance();
            cb.RegisterType<ConfigurableSingletonDatabaseUser>();
            var container = cb.Build();
            var sdu = container.Resolve<ConfigurableSingletonDatabaseUser>();
            var result = sdu.GetSumOfAllRooms(new[] {"osama" ,"ahmed"});
            Assert.AreEqual(result, 15);
        }
    }

}
