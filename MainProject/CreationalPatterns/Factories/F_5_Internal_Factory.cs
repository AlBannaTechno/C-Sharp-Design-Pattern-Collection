
// Here we will try to solve the violation happened when create a factory methods inside the class 
// because this violate the SingleResponsibility Principle  [ S : SOLID ]
// So We Will Move Those methods to another class 
// to do that we need to make the constructor public : so now any one can use this constructor 
// [[We will deal with this problem later]]

using System;

namespace MainProject.CreationalPatterns.Factories
{
    namespace Internal_Factory
    {
        internal class PointFactory
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
        internal class Point
        {
            private double x, y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }
        }
        
        public static class F5InternalFactory
        {
            public static void Run()
            {
                
            }
        }
    }
}