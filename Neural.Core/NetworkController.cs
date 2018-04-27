using System;
using Neural.Core.Providers;

namespace Neural.Core
{
    public class NetworkController : IDisposable
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
            settingsProvider.LayersSettingsChanged += LayersSettingsChangedHandler;
            ResetNetwork();
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
                if (i % 1000 == 0)
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

        private void ResetNetwork()
        {
            Network = new Network(settingsProvider.LayersSettings);
            UpdateNetworkHyperParameters();
        }
        
        private void UpdateNetworkHyperParameters()
        {
            Network.Moment = settingsProvider.Moment;
            Network.LearningRate = settingsProvider.LearningRate;
            Network.DropoutProbability = settingsProvider.DropoutProbability;
        }

        private void LayersSettingsChangedHandler(object sender, EventArgs args) => ResetNetwork();

        public void Dispose()
        {
            settingsProvider.LayersSettingsChanged -= LayersSettingsChangedHandler;
        }
    }
}
