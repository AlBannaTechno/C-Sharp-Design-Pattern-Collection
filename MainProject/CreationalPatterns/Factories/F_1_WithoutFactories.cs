using System;

namespace MainProject.CreationalPatterns.Factories
{
    public enum CoordinateSystems
    {
        Cartesian,
        Polar
    }
    class Point
    {
        private double a, b;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">Use This To : represent X | Rho</param>
        /// <param name="b">Use This To : represent Y | Theta</param>
        public Point(double a, double b, CoordinateSystems system = CoordinateSystems.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystems.Cartesian : 
                    this.a = a;
                    this.b = b;
                    break;
                case CoordinateSystems.Polar:
                    this.a = a * Math.Cos(b);
                    this.b = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system , "You Must Use Available coord System");
            }
            this.a = a;
            this.b = b;
        }
    }
    public static class F1WithoutFactories
    {
        public static void Run()
        {
            var point = new Point(5  , 10 , CoordinateSystems.Polar);
        }
    }
}