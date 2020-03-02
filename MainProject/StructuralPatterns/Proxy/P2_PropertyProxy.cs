using System;
using System.Collections.Generic;
using static System.Console;
namespace MainProject.StructuralPatterns.Proxy
{
    namespace P2PropertyProxy
    {
        /*
         * Domain specific behaviour of this class
         * to prevent setting the same value
         */
        class Property<T> : IEquatable<Property<T>> where T: new()
        {
            private T _value;

            public T Value
            {
                get => _value;
                set
                {
                    if (Equals(_value, value)) { return; }

                    WriteLine($"Assign Value : {value}");
                    _value = value;
                }
            }

            public Property() :
                // we can use default(T) but this will produce null for any reference type eg, string
                this(Activator.CreateInstance<T>())
            {

            }
            public Property(T value)
            {
                _value = value;
            }

            public static implicit operator T(Property<T> property)
            {
                return property._value;
            }

            public static implicit operator Property<T>(T value)
            {
                return new Property<T>(value);
            }

            #region Resharper Generated Code

            public bool Equals(Property<T> other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return EqualityComparer<T>.Default.Equals(_value, other._value);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Property<T>) obj);
            }

            public override int GetHashCode()
            {
                // because we use non-readonly values , this may be very dengrous
                // because generated hash will change every time we change the value
                return EqualityComparer<T>.Default.GetHashCode(_value);
            }

            public static bool operator ==(Property<T> left, Property<T> right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Property<T> left, Property<T> right)
            {
                return !Equals(left, right);
            }

            #endregion

        }


        class Student
        {
            private Property<float> _rate = new Property<float>();

            // [Solve] using `=` operator to not create a new Property<int>()
            public Property<float> Rate
            {
                get => _rate;
                set => _rate.Value = value;
            }
        }

        public static class PropertyProxyWorks
        {
            public static void Run()
            {
                var s = new Student();

                /*
                 * [Next Docs, describe the state if we use] {public Property<float> Rate { get; set; }}
                 * - [ Without back field].
                 *
                 * because , in C# there is no way to override `=` operator
                 * so `s.Rate = 10` will use implicit conversion => new Property<T>(value)
                 * so this will create a brand-new  Property<T>, instead of changing the existed one
                 * and this is because of implicit conversion nature
                 * s.Rate = new Property<float>()
                 */
                s.Rate = 10;
            }
        }
    }
}
