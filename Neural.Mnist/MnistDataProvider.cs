using System;
using System.Collections.Generic;
using System.Linq;
using Neural.Core.Providers;

namespace Neural.Mnist
{
    public class MnistDataProvider : IDataProvider
    {
        private static readonly Random R = new Random();

        public int InputNeuronsCount { get; } = 28 * 28;

        public int OutputNeuronsCount { get; } = 10;

        private List<double> ImageToList(byte[] image) => image.ToDoubles();

        private List<double> LabelToList(byte label) => Enumerable.Range(0, 10).Select(x => x == label ? 1D : 0D).ToList();

        private NetworkData ToNetworkData(byte[] image, byte label) => new NetworkData(ImageToList(image), LabelToList(label));

        public NetworkData GetTrainData()
        {
            var index = R.Next(MnistData.TrainImages.Count);
            return ToNetworkData(MnistData.TrainImages[index], MnistData.TrainLabels[index]);
        }

        public NetworkData GetTestData()
        {
            var index = R.Next(MnistData.TestImages.Count);
            return ToNetworkData(MnistData.TestImages[index], MnistData.TestLabels[index]);
        }

        public IEnumerable<NetworkData> GetAllTestData() =>
            MnistData.TestImages.Zip(MnistData.TestLabels, ToNetworkData);
    }
}
