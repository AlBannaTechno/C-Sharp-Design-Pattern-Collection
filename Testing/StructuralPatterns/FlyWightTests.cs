using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.dotMemoryUnit;
using MainProject.StructuralPatterns.FlyWight.F1RepeatingUserNames;
using NUnit.Framework;

namespace Testing.StructuralPatterns
{
    public class FlyWightTests
    {
        [TestFixture]
        public class RepeatingUserNames
        {
            public static void Run()
            {

            }

            [Test]
            public void TestUsers() // 9473460 / 7826028 : [40000/10100 items]
            {
                var firstNames = Enumerable.Range(0, 100)
                    .Select(_ => RandomString());
                var lastNames = Enumerable.Range(0, 100)
                    .Select(_ => RandomString());

                var users = new List<OptimizedUser>();
                foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                {
                    // to simulate duplication if not produced from randomization
                    users.Add(new OptimizedUser($"{firstName} {lastName}"));
                    users.Add(new OptimizedUser($"{firstName} {lastName}"));
                }

                ForceGC();

                dotMemory.Check(memory =>
                {
                    Console.WriteLine(memory.SizeInBytes);
                    Console.WriteLine(OptimizedUser.Count);
                    // OptimizedUser.PrintStrings();
                });
            }

            private void ForceGC()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            private string RandomString()
            {
                var random = new Random();
                return new string(
                    Enumerable.Range(0, 10).Select(i => (char)('a' + random.Next(26))).ToArray()
                );
            }
        }
    }
}
