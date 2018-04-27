using System;
using System.Collections.Generic;
using System.Linq;
using Neural.Core.Providers;

//using NeuralNetwork.Data;
//using NeuralNetwork.UI.Drawers;

namespace Neural.Mnist
{
    public class MnistDataProvider : IDataProvider
    {
        private static readonly Random R = new Random();

        public int InputNeuronsCount { get; } = 28 * 28;

        public int OutputNeuronsCount { get; } = 10;

        private readonly List<List<double>> trainInputs = MnistData.TrainImages.Select(x => x.ToDoubles()).ToList();

        private readonly List<List<double>> trainOutputs =
            MnistData.TrainLabels.Select(x => Enumerable.Range(0, 10).Select(y => y == x ? 1D : 0D).ToList()).ToList();

        private readonly List<List<double>> testInputs = MnistData.TestImages.Select(x => x.ToDoubles()).ToList();

        private readonly List<List<double>> testOutputs =
            MnistData.TestLabels.Select(x => Enumerable.Range(0, 10).Select(y => y == x ? 1D : 0D).ToList()).ToList();

        public NetworkData GetTrainData()
        {
            var index = R.Next(trainInputs.Count);
            return new NetworkData(trainInputs[index], trainOutputs[index]);
        }

        public NetworkData GetTestData()
        {
            var index = R.Next(testInputs.Count);
            return new NetworkData(testInputs[index], testOutputs[index]);
        }

        public IEnumerable<NetworkData> GetAllTestData() => testInputs.Zip(testOutputs, (i, o) => new NetworkData(i, o));
        
        //public IDrawer ResultDrawingFactory(List<double> input, List<double> expected, List<double> actual) => new MnistDrawer(input, expected, actual);
    }
}
