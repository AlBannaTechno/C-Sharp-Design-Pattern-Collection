using MainProject.CreationalPatterns.Singleton.S_2_Singleton_TestabilityIssues;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void SingletonDatabaseUserTest()
        {
            var sdu = new SingletonDatabaseUser();
            var result = sdu.GetSumOfAllRooms(new[] {"osama" ,"ahmed"});
            Assert.AreEqual(result, 1213 + 4223);
        }
    }
}
