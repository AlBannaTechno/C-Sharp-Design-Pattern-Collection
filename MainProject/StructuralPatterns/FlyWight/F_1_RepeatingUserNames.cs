using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.dotMemoryUnit;

// go to Testing.StructuralPatterns.FlyWight to show the difference
// you must know , in .net you will not see a big difference since in this example
// we demonstrate string interning which done automatically in .net framework as
// a resource optimization technique

namespace MainProject.StructuralPatterns.FlyWight
{
    namespace F1RepeatingUserNames
    {

    }
}

namespace MainProject.StructuralPatterns.FlyWight.F1RepeatingUserNames
{
    public class User
    {
        public string FullName { get; }

        public User(string fullName)
        {
            FullName = fullName;
        }
    }

    public class OptimizedUser
    {
        private static readonly List<string> Strings = new List<string>();
        private readonly int[] _nameIndices;

        public OptimizedUser(string fullName)
        {
            int getIndexOrAddString(string s)
            {
                int idx = Strings.IndexOf(s);
                if (idx != -1)
                {
                    return idx;
                }
                Strings.Add(s);
                return Strings.Count - 1;
            };

            var na = fullName.Split(' ');

            _nameIndices = na.Select(getIndexOrAddString).ToArray();
        }

        public string FullName => string.Join(" ", _nameIndices.Select(ind => Strings[ind]));

        public static int Count => Strings.Count;
        public static void PrintStrings()
        {
            foreach (var s in Strings)
            {
                Console.WriteLine($"{s},");
            }
        }
    }
}
