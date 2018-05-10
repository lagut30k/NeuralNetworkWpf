using System;
using System.Collections.Generic;
using System.Linq;
using Neural.Core.Activation;
using Neural.Core.Helpers;
using Neural.Core.Layers;

namespace Neural.Core
{
    public class Network
    {
        private OutputLayer OutputLayer => (OutputLayer)Layers.Last();

        private InputLayer InputLayer => (InputLayer)Layers.First();

        private IEnumerable<HiddenLayer> HiddenLayers => Layers.OfType<HiddenLayer>();

        public List<Layer> Layers { get; set; }

        public double LearningRate { get; set; } = 1;

        public double DropoutProbability { get; set; } = 0.3D;

        public double Moment { get; set; } = 1;

        public IActivationFunction Activation { get; } = new Sigmoid();

        public Network(List<LayerSettings> layersHyperParameters)
        {
            if (layersHyperParameters.Count < 2) return;

            var inputLayer = new InputLayer(layersHyperParameters.First(), this);
            Layers = new List<Layer>(layersHyperParameters.Count) { inputLayer };
            foreach (var layerParams in layersHyperParameters.Skip(1).Take(layersHyperParameters.Count - 2))
            {
                var prev = Layers.Last();
                Layers.Add(new HiddenLayer(layerParams, this, prev));
            }
            Layers.Add(new OutputLayer(layersHyperParameters.Last(), this, Layers.Last()));
        }

        public List<double> Run(List<double> input)
        {
            if (input.Count != Layers[0].Size) return null;

            InputLayer.AssignInput(input);
            foreach (var layer in Layers.Skip(1))
            {
                layer.Run();
            }
            return OutputLayer.GetResult();
        }

        public bool Train(List<double> input, List<double> idealOutput)
        {
            if ((input.Count != Layers.First().Size) || (idealOutput.Count != Layers.Last().Size)) return false;

            Dropout();

            Run(input);

            OutputLayer.InitDelta(idealOutput);
            foreach (var layer in HiddenLayers.Reverse())
            {
                layer.CalcDelta();
            }

            foreach (var layer in Layers.Skip(1))
            {
                layer.UpdateWeights();
            }

            ClearDropout();
            return true;
        }

        private void Dropout()
        {
            foreach (var hiddenLayer in HiddenLayers)
            {
                hiddenLayer.Dropout();
            }
        }

        private void ClearDropout()
        {
            foreach (var hiddenLayer in HiddenLayers)
            {
                hiddenLayer.ClearDropout();
            }
        }
    }
}
