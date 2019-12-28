using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MainProject.CreationalPatterns.Singleton
{
    namespace S_1_SingletonImplementation
    {
        public static class EnumerableExt
        {
            // Due to very low speed of my internet cable [internet speed 8 kb/s]
            // i can not install ManyLinq package
            // so i will write simple implementation of Batch Function To Use It Here
            public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> self, int sp)
            {
                if (sp <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(sp), "sp must bet > 0");
                }
                var mainList = new List<List<T>>();
                var list = self.ToList();
                List<T> subList;
                int c = 1;
                for (int i = 0; i < list.Count(); )
                {
                    subList = new List<T>();
                    for (int j = 0; j < sp & i < list.Count(); j++ , i++)
                    {
                        subList.Add(list[i]);
                    }
                    mainList.Add(subList);
                }

                return mainList;
            }
        }
        public interface IDatabase
        {
            int GetUserRoomNumber(string name);
        }

        public class SingletonDatabase : IDatabase
        {
            private Dictionary<string, int> _rooms;

            public SingletonDatabase()
            {
                Console.WriteLine("INIT DB");

                var rooms = File.ReadAllLines("./_LocalData//userRooms.txt").Batch(2)
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

        public static class SingletonImplementation
        {
            public static void Run()
            {

                var us = SingletonDatabase.Instance;
                var num = us.GetUserRoomNumber("muhammed");
                Console.WriteLine($"Muhammed Room Number : {num}");
                // var l = new List<string>(){"Osama","12","Ahmed","11", "Mhammed", "15"};
                // l.Batch(2);
            }
        }
    }
}
