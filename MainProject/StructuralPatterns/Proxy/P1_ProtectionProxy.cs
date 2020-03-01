using static System.Console;
namespace MainProject.StructuralPatterns.Proxy
{
    namespace P1ProtectionProxy
    {
        interface ICar
        {
            public void Drive();
        }

        class Car: ICar
        {
            public void Drive()
            {
                WriteLine("Start Driving");
            }
        }

        class Driver
        {
            public Driver(ushort age)
            {
                Age = age;
            }

            public ushort Age { get; set; }

        }
        class CarProxy : ICar
        {
            public Driver Driver { get;}
            private Car Car;
            public CarProxy(Driver driver)
            {
                Driver = driver;
                Car = new Car();
            }

            public void Drive()
            {
                if (Driver.Age < 16)
                {
                    WriteLine("You can, not drive");
                    return;
                }
                Car.Drive();
            }
        }

        static class ProtectionProxy
        {
            public static void Run()
            {
                var car = new CarProxy(new Driver(18));
                car.Drive();
            }
        }
    }
}
