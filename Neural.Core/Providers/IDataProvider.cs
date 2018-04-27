using System;
using System.Collections.Generic;
using System.Linq;
using Neural.Core.Helpers;

//using NeuralNetwork.UI.Drawers;

namespace Neural.Core.Providers
{
    public interface IDataProvider
    {
        int InputNeuronsCount { get; }

        int OutputNeuronsCount { get; }

        NetworkData GetTrainData();

        NetworkData GetTestData();

        IEnumerable<NetworkData> GetAllTestData();

        //IDrawer ResultDrawingFactory(List<double> input, List<double> expected, List<double> actual);
    }
}
