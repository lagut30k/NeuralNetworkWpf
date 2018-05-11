using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Neural.Core.Neurons;

namespace Neural.Core.Layers
{
    public abstract class Layer
    {
        private static readonly RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public List<Neuron> Neurons { get; set; }

        public bool HasBias { get; }

        public int Size => Neurons.Count;

        public Layer PreviousLayer { get; }

        public Layer NextLayer { get; set; }

        public Network Network { get; }

        protected abstract Neuron NeuronFactory(int i);

        public void InitNeuronsWeights()
        {
            foreach (var neuron in Neurons)
            {
                neuron.InitWeights();
            }
        }

        protected Layer(LayerSettings layerSettings, Network network, Layer previousLayer = null)
        {
            Network = network;
            HasBias = layerSettings.HasBias;
            PreviousLayer = previousLayer;
            Neurons = Enumerable.Range(0, layerSettings.NeuronsCount)
                .Select(NeuronFactory)
                .ToList();

            if (previousLayer != null)
            {
                PreviousLayer.NextLayer = this;
            }
        }

        public void Dropout()
        {
            Parallel.ForEach(Neurons, neuron =>
            {
                var b = new byte[1];
                rngCsp.GetBytes(b);
                neuron.Dropped = Convert.ToBoolean(b[0] & 0b1);
            });
        }

        public void ClearDropout()
        {
            Parallel.ForEach(Neurons, neuron =>
            {
                neuron.Dropped = false;
            });
        }

        public virtual void Run()
        {
            Parallel.ForEach(Neurons, neuron =>
            {
                neuron.CalcValue();
            });
        }

        public virtual void CalcDelta()
        {
            Parallel.ForEach(Neurons, neuron =>
            {
                neuron.CalcDelta();
            });
        }
        public void UpdateWeights()
        {
            Parallel.ForEach(Neurons, neuron =>
            {
                neuron.UpdateWeights();
            });
        }

        //public void Dropout()
        //{
        //    foreach (var neuron in Neurons)
        //    {
        //        if (Network.R.NextDouble() < Network.DropoutProbability)
        //        {
        //            neuron.Dropped = true;
        //        }
        //    }
        //}

        //public void ClearDropout()
        //{
        //    foreach (var neuron in Neurons)
        //    {
        //        neuron.Dropped = false;
        //    }
        //}

        //public virtual void Run()
        //{
        //    foreach (var neuron in Neurons)
        //    {
        //        neuron.CalcValue();
        //    }
        //}

        //public virtual void CalcDelta()
        //{
        //    foreach (var neuron in Neurons)
        //    {
        //        neuron.CalcDelta();
        //    }
        //}

        //public void UpdateWeights()
        //{
        //    foreach (var neuron in Neurons)
        //    {
        //        neuron.UpdateWeights();
        //    }
        //}
    }
}
