using System;
using System.Collections.Generic;
using Neural.Core;
using Neural.Core.Providers;

namespace NeuralWpf.Providers
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly MainVm viewModel;
        
        public SettingsProvider(MainVm viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler LayersSettingsChanged;

        public List<LayerSettings> LayersSettings { get; } = new List<LayerSettings>
        {
            new LayerSettings {NeuronsCount = 784, HasBias = false},
            new LayerSettings {NeuronsCount = 300, HasBias = true},
            new LayerSettings {NeuronsCount = 10, HasBias = true},
        };

        public double LearningRate => viewModel.LearningRate;

        public double Moment => viewModel.Moment;

        public double DropoutProbability => viewModel.DropoutProbability;

        public int TrainLoops => viewModel.TrainLoops;
    }
}
