using System.Collections.Generic;

namespace Neural.Core.Providers
{
    public interface IDataProvider
    {
        int InputNeuronsCount { get; }

        int OutputNeuronsCount { get; }

        NetworkData GetTrainData();

        NetworkData GetTestData();

        IEnumerable<NetworkData> GetAllTestData();
    }
}
