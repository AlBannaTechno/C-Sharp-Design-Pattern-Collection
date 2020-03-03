using System;
using System.Linq;
using MoreLinq;

namespace MainProject.StructuralPatterns.Proxy
{
    namespace P5CompositeProxyWithArrayBackedProperties
    {
        public static class ImmutableExtensions
        {
            public static void ForEachImmutable<T>(this T[] arr, T val)
            {
                for (var i = 0; i < arr.Length; i++)
                {
                    arr[i] = val;
                }
            }

            public static void ForEachImmutable<T>(this T[] arr, Func<T,T> func)
            {
                for (var i = 0; i < arr.Length; i++)
                {
                    arr[i] = func(arr[i]);
                }
            }
        }
        public class CarFixOptions
        {
            private readonly bool[] _flags = new bool[3];

            public bool? All
            {
                get
                {
                    if (_flags.Skip(1).All(f => f == _flags[0]))
                    {
                        return _flags[0];
                    }
                    return null;
                }
                set
                {
                    if (!value.HasValue) { return; }
                    _flags.ForEachImmutable(value.Value);
                }
            }
            public bool Lights
            {
                get => _flags[0];
                set => _flags[0] = value;
            }

            public bool Tiers
            {
                get => _flags[1];
                set => _flags[1] = value;
            }

            public bool Motor
            {
                get => _flags[1];
                set => _flags[1] = value;
            }
        }

        public static class CPAB
        {
            public static void Run()
            {
                var cps = new CarFixOptions();
                cps.All = true;
                Console.WriteLine(cps.All);
            }
        }
    }
}
