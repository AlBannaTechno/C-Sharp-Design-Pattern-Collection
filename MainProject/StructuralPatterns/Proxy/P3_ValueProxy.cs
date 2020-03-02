using System;
using System.Diagnostics;
using static System.Console;
namespace MainProject.StructuralPatterns.Proxy
{
    namespace P3ValueProxy
    {

        [DebuggerDisplay("{_value * 100.0f}%")]
        struct Percentage : IEquatable<Percentage>
        {
            private readonly float _value;

            // there is no obvious reason of exposing this constructor
            // but there is no way around this, so we will use internal
            internal Percentage(float value)
            {
                _value = value;
            }

            public static float operator *(float f, Percentage p )
            {
                return f * p._value;
            }

            public static float operator *(Percentage p, float f)
            {
                return f * p._value;
            }

            public static Percentage operator +(Percentage p1, Percentage p2)
            {
                return new Percentage(p1._value + p2._value);
            }

            public override string ToString()
            {
                return $"{_value * 100}%";
            }

            #region Rider Generated Code

            public bool Equals(Percentage other)
            {
                return _value.Equals(other._value);
            }

            public override bool Equals(object obj)
            {
                return obj is Percentage other && Equals(other);
            }

            public override int GetHashCode()
            {
                return _value.GetHashCode();
            }

            public static bool operator ==(Percentage left, Percentage right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Percentage left, Percentage right)
            {
                return !left.Equals(right);
            }

            #endregion
        }

        static class PercentageExtensions
        {
            public static Percentage Percent(this float value)
            {
                return new Percentage(value / 100.0f);
            }
            public static Percentage Percent(this int value)
            {
                return new Percentage(value / 100.0f);
            }
        }
        public static class ValueProxy
        {
            public static void Run()
            {
                WriteLine(10f * 5.Percent());
                WriteLine(2.Percent() + 5.Percent());
                WriteLine(5.Percent() * 5);
            }
        }
    }
}
