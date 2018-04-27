using System;
using System.Collections.Generic;
using System.Linq;
using Neural.Core.Neurons;

namespace Neural.Core.Layers
{
    public class OutputLayer : Layer
    {
        public OutputLayer(LayerSettings layerSettings, Network network, Layer prevLayer)
            : base(layerSettings, network, prevLayer)
        {
            InitNeuronsWeights();
        }

        protected override Neuron NeuronFactory(int i) => new OutputNeuron(this, i);

        public override void CalcDelta()
        {
            throw new NotImplementedException();
        }

        public List<double> GetResult()
        {
            return Neurons.Select(x => x.Value).ToList();
        }

        public void InitDelta(List<double> idealOutput)
        {
            foreach (var (neuron, output) in Neurons.Cast<OutputNeuron>().Zip(idealOutput, (neuron, output) => (neuron, output)))
            {
                neuron.InitDelta(output);
            }
        }
    }
}
