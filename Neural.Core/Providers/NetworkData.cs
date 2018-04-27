using System.Collections.Generic;

namespace Neural.Core.Providers
{
    public class NetworkData
    {
        public List<double> Input { get; }

        public List<double> Output { get; }

        public NetworkData(List<double> input, List<double> output)
        {
            Input = input;
            Output = output;
        }
    }
}
