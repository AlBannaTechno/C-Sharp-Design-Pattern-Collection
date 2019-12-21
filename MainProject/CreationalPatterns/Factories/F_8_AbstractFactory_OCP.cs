using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Console;
namespace MainProject.CreationalPatterns.Factories
{
    namespace F7AbstractFactoryWorksOCP
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
          
            private readonly List<(string hotDrinkTypeName, IHotDrinkFactory hotDrinkFactory)> _factories  
                = new List<(string hotDrinkTypeName, IHotDrinkFactory hotDrinkFactory)>();
            
            public HotDrinkMachine(Assembly [] assemblyList = null , bool addInternalAssembly = true)
            {
                var allAssemblies = new List<Assembly>();
                
                if (addInternalAssembly)
                {
                    allAssemblies.Add(typeof(HotDrinkMachine).Assembly);
                }
                
                if (assemblyList != null)
                {
                    allAssemblies.AddRange(assemblyList);
                }

                foreach (var assembly in allAssemblies)
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (typeof(IHotDrinkFactory).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            _factories.Add((type.Name.Replace("Factory", string.Empty),(IHotDrinkFactory) Activator.CreateInstance(type)));
                        }
                    }
                }
            }

            public IHotDrink MakeDrink(string drinkName, int amount)
            {
                var drink = _factories.FirstOrDefault(f => 
                    f.hotDrinkTypeName.Equals(drinkName, StringComparison.OrdinalIgnoreCase));
                return drink.hotDrinkFactory.Prepare(amount);
            }
        }

        #endregion
        
        
        public static class F7AbstractFactory
        {
            public static void Run()
            {
                // var mc = new HotDrinkMachine(); // equal to next line
                var mc = new HotDrinkMachine(new []{ typeof(F7AbstractFactory).Assembly}, false);
                var tea = mc.MakeDrink("tea", 10);
                tea.Consume();
            }
        }
    }
  
}