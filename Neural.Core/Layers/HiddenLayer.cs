using Neural.Core.Neurons;

namespace Neural.Core.Layers
{
    public class HiddenLayer : Layer
    {
        public HiddenLayer(LayerSettings layerSettings, Network network, Layer prevLayer) 
            : base(layerSettings, network, prevLayer)
        {
        }

        protected override Neuron NeuronFactory(int i) => new HiddenNeuron(this, i);
    }
}
