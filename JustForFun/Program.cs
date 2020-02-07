using System;
using System.Text;

namespace JustForFun
{

    public class ConstrainedValue<T> where T : IComparable, IComparable<T>, IEquatable<T>
    {
        private readonly T _value;

        public ConstrainedValue(T value)
        {
            _value = value;
        }

        public static T operator + (ConstrainedValue<T> x, ConstrainedValue<T> y)
        {
            return (dynamic)x._value + y._value;
        }

        public static T operator - (ConstrainedValue<T> x, ConstrainedValue<T> y)
        {
            return (dynamic)x._value - y._value;
        }

        public static T operator * (ConstrainedValue<T> x, ConstrainedValue<T> y)
        {
            return (dynamic)x._value * y._value;
        }

        public static T operator / (ConstrainedValue<T> x, ConstrainedValue<T> y)
        {
            return (dynamic)x._value / y._value;
        }
    }


    class TestEq<T> where T: IConvertible, IEquatable<int>
    {
        // public TestEq(T x, T y)
        // {
        //
        //     var r = x + y;
        // }
    }
    class XXX : IEquatable<int>
    {
        public bool Equals(int other)
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        class TestCons<T> where T : IComparable, IComparable<T>, IEquatable<T>
        {
            public TestCons(T x)
            {
                var m = new ConstrainedValue<T>(x);
            }
        }
        static void Main(string[] args)
        {

            // var r = new TestEq<int>();
            // var x = new Vec();
            // var r= x + x *x %x;
            // Console.WriteLine($"Hello World! {r}");


        }
    }
}
