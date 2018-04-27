using System;
using System.Collections.Generic;
using System.Linq;
using Neural.Core.Neurons;

namespace Neural.Core.Layers
{
    public class InputLayer : Layer
    {
        public InputLayer(LayerSettings layerSettings, Network network) : base(layerSettings, network)
        {
        }

        public void AssignInput(List<double> input)
        {
            foreach (var (neuron, d) in Neurons.Zip(input, (neuron, d) => (neuron, d)))
            {
                neuron.Value = d;
            }
        }

        protected override Neuron NeuronFactory(int i) => new InputNeuron(this, i);

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override void CalcDelta()
        {
            throw new NotImplementedException();
            //foreach (var neuron in Neurons)
            //{
            //    neuron.CalcDelta();
            //}
        }
    }
}