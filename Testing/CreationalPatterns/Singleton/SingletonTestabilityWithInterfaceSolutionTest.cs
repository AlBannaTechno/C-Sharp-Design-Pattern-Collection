using System.Collections.Generic;
using MainProject.CreationalPatterns.Singleton.S_1_SingletonImplementation;
using MainProject.CreationalPatterns.Singleton.S_3_Singleton_TestabilityWithInterfaceSolution;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class SingletonTestabilityWithInterfaceSolutionTest
    {
        [Test]
        public void Test()
        {
            var sdu = new ConfigurableSingletonDatabaseUser(new DummyDatabase());
            var result = sdu.GetSumOfAllRooms(new[] {"osama" ,"ahmed"});
            Assert.AreEqual(result, 15);
        }
    }

    class DummyDatabase: IDatabase
    {
        public int GetUserRoomNumber(string name)
        {
            return new Dictionary<string , int>()
            {
                {"osama", 5},
                {"ahmed", 10}
            }[name];
        }
    }
}
