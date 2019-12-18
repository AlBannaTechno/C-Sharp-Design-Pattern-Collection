using System;

namespace JustForFun
{

    class Program
    {
        static void Main(string[] args)
        {
            var x = new Vec();
            var r= x + x *x %x;
            Console.WriteLine($"Hello World! {r}");
        }
    }
}