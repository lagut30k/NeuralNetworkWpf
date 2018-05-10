using System;
using System.Linq;
using Neural.Core.Layers;
using Neural.Core.Providers;

namespace Neural.Core
{
    public class NetworkController
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly Func<IDataProvider> dataProviderFactory;

        private bool stopFlag;

        public event EventHandler ReadyToRun;
        public event EventHandler ReadyToFullTest;

        public Network Network { get; private set; }

        public NetworkController(ISettingsProvider settingsProvider, Func<IDataProvider> dataProviderFactory)
        {
            this.settingsProvider = settingsProvider;
            this.dataProviderFactory = dataProviderFactory;
            UpdateNetworkHyperParameters();
        }
        
        public void Train()
        {
            stopFlag = false;
            var dataProvider = dataProviderFactory();
            var trainLoops = settingsProvider.TrainLoops;
            UpdateNetworkHyperParameters();
            for (var i = 0; i < trainLoops && !stopFlag; i++)
            {
                var data = dataProvider.GetTrainData();
                Network.Train(data.Input, data.Output);
                if (i % 250 == 0)
                {
                    ReadyToRun?.Invoke(this, null);
                }
                if ((i + 1) % 10000 == 0)
                {
                    ReadyToFullTest?.Invoke(this, null);
                }
            }
        }

        public void Stop()
        {
            stopFlag = true;
        }

        private bool NeedToReinitNetwork()
        {
            if (settingsProvider.LayersSettings.Count != Network?.Layers?.Count)
            {
                return true;
            }
            return settingsProvider.LayersSettings.Where((t, i) => !IsLayerChanged(t, Network.Layers[i])).Any();
        }

        private bool IsLayerChanged(LayerSettings settings, Layer layer)
        {
            if (settings.NeuronsCount != layer.Size)
            {
                return false;
            }
            return layer is InputLayer || settings.HasBias == layer.HasBias;
        }
        
        private void UpdateNetworkHyperParameters()
        {
            if (NeedToReinitNetwork())
            {
                Network = new Network(settingsProvider.LayersSettings);
            }
            Network.Moment = settingsProvider.Moment;
            Network.LearningRate = settingsProvider.LearningRate;
            Network.DropoutProbability = settingsProvider.DropoutProbability;
        }
    }
}
