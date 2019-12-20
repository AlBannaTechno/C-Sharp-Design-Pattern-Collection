
// Here we will try to solve the violation happened when create a factory methods inside the class 
// because this violate the SingleResponsibility Principle  [ S : SOLID ]
// So We Will Move Those methods to another class 
// to do that we need to make the constructor public : so now any one can use this constructor 
// [[We will deal with this problem later]]

using System;

namespace MainProject.CreationalPatterns.Factories
{
    namespace InnerFactory
    {
        internal class Point
        {
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
            
            public static Point Origin = new Point(0, 0); // better : only create one point
            // public static Point Origin => new Point(0, 0); // getter : [] create point with every call
            
            // .net core implementation use this method
            public static class Factory
            {
                public static Point NewCartesianPoint(double x, double y)
                {
                    return new Point(x, y);
                }

                public static Point NewPolarPoint(double rho, double theta)
                {
                    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
                }
            }
        }
        
        public static class F4Factory
        {
            public static void Run()
            {
                var p = Point.Factory.NewPolarPoint(15, 14);
                var org = Point.Origin;
            }
        }
    }
}