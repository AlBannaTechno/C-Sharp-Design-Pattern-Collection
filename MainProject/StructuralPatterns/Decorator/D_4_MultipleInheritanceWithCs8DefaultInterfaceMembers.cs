using static System.Console;
namespace MainProject.CreationalPatterns.Decorator
{
    namespace D4MultipleInheritanceWithCs8DefaultInterfaceMembers
    {
        interface ICreature
        {
            int Age { get; set; }
            private int Height
            {
                get { return Age * 2;}
                set { /**/ }
            }

            void Speak()
            {
                if (Age > 1000) // :)
                {
                    WriteLine("Start Speaking :))))");
                }
                else
                {
                    Age += 10;
                }
            }

        }
        internal interface IAnimal : ICreature
        {
            float Weight { get; set; }
            public void Walk()
            {
                WriteLine($"Walk With Weight {Weight}");
            }

        }

        class Animal : IAnimal
        {
            public float Weight { get; set; }

            public int Age { get; set; }
        }

        internal interface IBird : ICreature
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

            public int Age { get; set; }
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

            private Animal _animal = new Animal();
            private Bird _bird = new Bird();
            private float _weight;
            public int Age { get; set; }
        }

        public static class MIDCDM
        {
            public static void Run()
            {
                var duck = new Duck();
                ((ICreature)duck).Speak();
                (duck as ICreature).Speak();
                if (duck is ICreature c)
                {
                    c.Speak();
                }

                if (duck is IAnimal animal)
                {
                    animal.Walk();
                }
            }
        }
    }
}
