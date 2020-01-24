using static System.Console;
namespace MainProject.CreationalPatterns.Decorator
{
    namespace D3MultipleInheritanceWithInterfaces
    {
        internal interface IAnimal
        {
            float Weight { get; set; }
            void Walk();
        }

        class Animal : IAnimal
        {
            public float Weight { get; set; }

            public void Walk()
            {
                WriteLine($"Walk With Weight {Weight}");
            }
        }

        internal interface IBird
        {
            float Weight { get; set; }
            void Fly();
        }

        class Bird : IBird
        {
            public float Weight { get; set; }

            public void Fly()
            {
                WriteLine($"Fly With Weight {Weight}");
            }
        }

        class Duck : IAnimal, IBird
        {
            public float Weight
            {
                get => _weight;
                set
                {
                    _weight = value;
                    _bird.Weight = value;
                    _animal.Weight = value;
                }
            }

            public void Fly()
            {
                _bird.Fly();
            }

            public void Walk()
            {
                _animal.Walk();
            }

            private Animal _animal = new Animal();
            private Bird _bird = new Bird();
            private float _weight;
        }
        public static class MultipleInheritanceWithInterfaces
        {
            public static void Run()
            {
                var duck = new Duck {Weight = 2.3f};
                duck.Fly();
                duck.Walk();
            }
        }
    }
}
