using System;
using System.Collections.Generic;

namespace Neural.Core.Providers
{
    public interface ISettingsProvider
    {
        event EventHandler LayersSettingsChanged;

        List<LayerSettings> LayersSettings { get; }

        double LearningRate { get; }

        double Moment { get; }

        double DropoutProbability { get; }

        int TrainLoops { get; }
    }
}
