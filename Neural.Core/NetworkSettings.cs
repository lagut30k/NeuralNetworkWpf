using System.Collections.Generic;

namespace Neural.Core
{
    public class NetworkSettings
    {
        public double LearningRate { get; set; }

        public double Moment { get; set; }

        public List<LayerSettings> LayersSettings {get; set; }
    }
}
