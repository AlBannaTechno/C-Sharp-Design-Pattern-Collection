using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MainProject.CreationalPatterns.Composite
{
    namespace C2NeuralNetworkProblem
    {
        class Neuron
        {
            public float Value;
            public List<Neuron> In, Out;

            public void ConnectTo(Neuron other)
            {
                Out.Add(other);
                other.In.Add(this);
            }
        }

        class NeuronLayer : Collection<Neuron>
        {

        }

        class NeuronRing : Collection<Neuron>
        {

        }

        // ....... many Neuron collections ..
        public class NeuralNetworkProblem
        {
            public static void Run()
            {
                // 1st case : connect neuron to other neuron
                var neuron1= new Neuron(){Value = 2.3f};
                var neuron2= new Neuron(){Value = 0.3f};
                neuron1.ConnectTo(neuron2);

                // 2nd , 3 , 4 , cases : Connect single neuron to scalar neurons [collection of neurons] {NeuronLayer}
                // In the current solution we must create 4 different methods to implement ConnectTo
                // So we should use composite pattern [C_3_NeuralNetworkSolution]
            }
        }
    }
}
