using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MainProject.CreationalPatterns.Singleton.S_1_SingletonImplementation;
using NUnit.Framework;

namespace MainProject.CreationalPatterns.Singleton
{
    namespace S_2_Singleton_TestabilityIssues
    {

        class SingletonDatabase : IDatabase
        {
            private static int _instanceCount = 0;
            private Dictionary<string, int> _rooms;

            public static int Count => _instanceCount;
            public SingletonDatabase()
            {
                Console.WriteLine("INIT DB");
                _instanceCount++;
                var file = File.ReadAllLines(
                    // to get the location in case of executing tests
                    Path.Combine(
                        new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,
                        "_LocalData//userRooms.txt"
                        )
                    );
                var rooms = file.Batch(2)
                    .ToDictionary(ls => ls.ElementAt(0), ls => int.Parse(ls.ElementAt(1)));
                _rooms = rooms;
            }

            public int GetUserRoomNumber(string name)
            {
                return _rooms[name];
            }

            private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
            public static SingletonDatabase Instance => instance.Value;
        }

        class SingletonDatabaseUser
        {
            public int GetSumOfAllRooms(IEnumerable<string> rooms)
            {
                int result = 0;
                foreach (var room in rooms)
                {
                    result += SingletonDatabase.Instance.GetUserRoomNumber(room);
                }

                return result;
            }
        }

        public static class SingletonTestabilityIssues
        {
            public static void Run()
            {
                // refer to SingletonTests.cs in Testing project
            }
        }
    }
}
