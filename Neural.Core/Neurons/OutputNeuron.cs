using System;
using Neural.Core.Layers;

namespace Neural.Core.Neurons
{
    public class OutputNeuron : Neuron
    {
        public OutputNeuron(Layer layer, int index) : base(layer, index)
        {
        }

        public override void CalcDelta()
        {
            throw new NotImplementedException();
        }

        public void InitDelta(double idealOutput)
        {
            Delta = Network.Activation.CalcGradByValue(Value) * (idealOutput - Value);
        }
    }
}
