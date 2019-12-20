using System;

namespace MainProject.CreationalPatterns.Factories
{
    namespace F2FactoryMethod
    {
        internal class Point
            {
                public static Point NewCartesianPoint(double x, double y)
                {
                    return new Point(x, y);
                }
                
                public static Point NewPolarPoint(double rho, double theta)
                {
                    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
                }
                private double x, y;

                private Point(double x, double y)
                {
                    this.x = x;
                    this.y = y;
                }

                public override string ToString()
                {
                    return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
                }
            }
        public class F2FactoryMethod
        {
            public static void Run()
            {
                var point = Point.NewCartesianPoint(10 , 40);
                Console.WriteLine(point);
            }
        }
    }
}