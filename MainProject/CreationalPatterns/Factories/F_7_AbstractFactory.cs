using System;
using System.Collections.Generic;
using static System.Console;
namespace MainProject.CreationalPatterns.Factories
{
    namespace F7AbstractFactoryWorks
    {
        #region IHotDrink

        public interface IHotDrink
        {
            void Consume();
        }

        internal class Tea : IHotDrink
        {
            public void Consume()
            {
                WriteLine("Consume Tea");
            }
        }
        
        internal class Coffee : IHotDrink
        {
            public void Consume()
            {
                WriteLine("Consume Coffee");
            }
        }

        #endregion



        #region IHotDrinkFactory

        public interface IHotDrinkFactory
        {
            IHotDrink Prepare(int amount);
        }

        internal class TeaFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                WriteLine($"Prepare Tea : With : - {amount}");
                return new Tea();
            }
        }
        
        internal class CoffeeFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                WriteLine($"Prepare Coffee : With : - {amount}");
                return new Coffee();
            }
        }

        #endregion


        #region HotDrinkMachine

        public class HotDrinkMachine
        {
            public enum AvailableDrink
            {
                Tea, Coffee
            }
            private readonly Dictionary<AvailableDrink, IHotDrinkFactory> _factories = 
                new Dictionary<AvailableDrink, IHotDrinkFactory>();

            public HotDrinkMachine()
            {
                foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
                {
                    var assemblyName =typeof(HotDrinkMachine).Namespace ; // to get the same namespace
                    var enumName = Enum.GetName(typeof(AvailableDrink), drink);
                    var totalPath = assemblyName + "." + enumName + "Factory" ;
                    var type = Type.GetType(totalPath);
                    // will always not null
                    if (type != null)
                    {
                        var factory = (IHotDrinkFactory) Activator.CreateInstance(type);
                        _factories.Add(drink,factory);
                    }
                }
            }

            public IHotDrink MakeDrink(AvailableDrink drink, int amount)
            {
                return _factories[drink].Prepare(amount);
            }
        }

        #endregion
        
        
        public static class F7AbstractFactory
        {
            public static void Run()
            {
                var mc = new HotDrinkMachine();
                var d = mc.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 200);
                d.Consume();
            }
        }      
    }
  
}