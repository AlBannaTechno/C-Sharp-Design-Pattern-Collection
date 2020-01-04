using System;

namespace MainProject.CreationalPatterns.Singleton
{
    // here we will addressing monostate pattern
    // may considered as a sub type of singleton
    // but its not anti-pattern like singleton
    // its work will with DI without breaking singleton behaviour
    // https://stackoverflow.com/questions/887317/monostate-vs-singleton
    public class S4SingletonMonostate
    {
        public static void Run()
        {
            var ceo1 = new CEO();
            ceo1.Name = "Osama";
            ceo1.Age = 23;
            var ceo2 = new CEO();

            Console.WriteLine(ceo2.Name); //
        }
    }


    // There is just one CEO for any company
    public class CEO
    {
        private static string _name;
        private int _age;

        public  string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }
    }
}
