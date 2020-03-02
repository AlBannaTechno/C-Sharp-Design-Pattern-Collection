using System.Collections.Generic;

namespace MainProject.StructuralPatterns.Proxy
{
    namespace P4CompositeProxySoAAoS
    {
        class Creatures
        {
            private readonly int _size;
            private byte[] _age;
            private int[] _x;
            private int[] _y;

            public Creatures(int size)
            {
                _size = size;
                _age = new byte[size];
                _x = new int[size];
                _y = new int[size];
            }

            internal struct CreatureProxy
            {
                private readonly Creatures _creatures;
                private readonly int _index;

                public CreatureProxy(Creatures creatures, int index)
                {
                    _creatures = creatures;
                    _index = index;
                }

                public ref byte Age => ref _creatures._age[_index];
                public ref int X => ref _creatures._x[_index];
                public ref int Y => ref _creatures._y[_index];
            }

            public IEnumerator<CreatureProxy> GetEnumerator()
            {
                for (int i = 0; i < _size; i++)
                {
                    yield return new CreatureProxy(this, i);
                }
            }
        }
        class Creature
        {
            public byte Age;
            public int X, Y;
        }
        public static class CompositeProxySoAAoS
        {
            public static void Run()
            {
                // AoS : Array Of Structures
                Creature[] creatures = new Creature[100];
                foreach (Creature creature in creatures)
                {
                    /*
                     *
                     * here is a performance problem
                     * if we ignore .net wrapper around Creature object
                     * the representation in ram will be [Age X Y Age X Y ......]
                     * so processor pointer need to jump know steps to locate the location of values
                     * but this will be more efficient if we store values like
                     * [Age Age Age ...], [X X X ...], [Y Y Y ...]
                     * so we need to separate it in different arrays
                     * and this what we will do in Creatures Class
                     *
                     */
                    creature.Y++;
                }

                // SoA : Structure Of Arrays
                var hpCreatures = new Creatures(100);
                foreach (Creatures.CreatureProxy creature in hpCreatures)
                {
                    creature.X++;
                }

            }
        }
    }
}
