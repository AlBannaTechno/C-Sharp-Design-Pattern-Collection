using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MainProject.CreationalPatterns.Composite
{
    namespace C3NeuralNetworkSolution
    {
        // if we work with other languages does support multi inheritance , we can create
        // base class for Neuron and NeuronNetwork
        // but here we can not do that because C# does not support multi inheritance
        // and NeuronLayer Already Implement Collection
        // so here we will use extension method
        static class NeuronExtensions
        {
            public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
            {
                if (ReferenceEquals(self, other))
                {
                    return;
                }
                foreach (var selfNeuron in self)
                {
                    foreach (var otherNeuron in other)
                    {
                        selfNeuron.Out.Add(otherNeuron);
                        otherNeuron.In.Add(selfNeuron);
                    }
                }
            }
        }
        class Neuron : IEnumerable<Neuron>
        {
            public float Value;
            public List<Neuron> In, Out;

            public void ConnectTo(Neuron other)
            {
                Out.Add(other);
                other.In.Add(this);
            }

            public IEnumerator<Neuron> GetEnumerator()
            {
                yield return this; // here is the solution
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class NeuronLayer : Collection<Neuron>
        {

        }
        public class NeuralNetworkSolution
        {
            public static void Run()
            {
                var neuron1= new Neuron(){Value = 2.3f};
                var neuron2= new Neuron(){Value = 0.3f};
                var neuronLayer1 = new NeuronLayer();
                var neuronLayer2 = new NeuronLayer();
                neuronLayer1.Add(new Neuron(){Value = 0.54f});
                neuronLayer2.Add(new Neuron(){Value = 1.3f});
                neuronLayer2.Add(new Neuron(){Value = 4.6f});

                // 1
                neuron1.ConnectTo(neuron2);
                // 2
                neuronLayer1.ConnectTo(neuron1);
                // 3
                neuron2.ConnectTo(neuronLayer1);
                // 4
                neuronLayer1.ConnectTo(neuronLayer2);
            }
        }
    }
}
