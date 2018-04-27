using System.Collections.Generic;
using System.Linq;
using Neural.Core.Layers;

namespace Neural.Core.Neurons
{
    public abstract class Neuron
    {
        protected Layer Layer { get; set; }

        protected Network Network => Layer.Network;

        protected readonly int index;

        public List<double> Weights { get; set; }

        public double Bias { get; set; }
        
        public double Delta { get; set; }
        
        public double Value { get; set; }

        public bool Dropped { get; set; }

        protected Neuron(Layer layer, int index)
        {
            this.index = index;
            Layer = layer;
            Weights = Enumerable.Repeat(0D, layer.PreviousLayer?.Size ?? 0).ToList();
        }

        public virtual void CalcValue()
        {
            if (Dropped)
            {
                Value = 0;
                return;
            }
            var prevLayerValues = Layer.PreviousLayer.Neurons.Select(n => n.Value);
            var totalInput = Weights.Zip(prevLayerValues, (w, input) => w * input).Sum() + (Layer.HasBias ? Bias : 0)
                / (1 - Layer.Network.DropoutProbability);
            Value = Network.Activation.CalcValue(totalInput);
        }

        public virtual void CalcDelta()
        {
            Delta = Dropped ? 0 : Network.Activation.CalcGradByValue(Value) * Layer.NextLayer.Neurons.Sum(x => x.Weights[index] * x.Delta);
        }

        public void UpdateWeights()
        {
            if (Dropped)
            {
                return;
            }

            var learningRate = Layer.Network.LearningRate;
            if (Layer.HasBias)
            {
                Bias += learningRate * Delta;
            }
            for (var k = 0; k < Weights.Count; k++)
                Weights[k] += Layer.PreviousLayer.Neurons[k].Value * learningRate * Delta;
        }

        public void InitWeights()
        {
            var prevLayerSize = Layer.PreviousLayer?.Size ?? 0;
            var nextLayerSize = Layer.PreviousLayer?.Size ?? 0;
            for (var i = Weights.Count - 1; i >= 0; i--)
            {
                Weights[i] = Network.Activation.GetInitWeight(prevLayerSize, nextLayerSize);
            }
        }
    }
}
