using System;
using System.Collections.Generic;
using System.Linq;
using Neural.Core.Providers;

namespace NeuralWpf.Providers.Data
{
    public class LogicalOperationDataProvider : IDataProvider
    {
        private static readonly Random R = new Random();
        private readonly LogicalOperatorComboBoxItem[] generatorOptions =
            {
                new LogicalOperatorComboBoxItem {Text = "XOR", Generator = (a, b) => a ^ b},
                new LogicalOperatorComboBoxItem {Text = "AND", Generator = (a, b) => a && b},
                new LogicalOperatorComboBoxItem {Text = "OR", Generator = (a, b) => a || b},
                new LogicalOperatorComboBoxItem {Text = "=>", Generator = (a, b) => !a || b},
                new LogicalOperatorComboBoxItem {Text = "<=", Generator = (a, b) => a || !b},
                new LogicalOperatorComboBoxItem {Text = "<=>", Generator = (a, b) => a == b},
            };

        private Func<bool, bool, bool> generator;
        private int internalVal;

        public int InputNeuronsCount { get; } = 2;

        public int OutputNeuronsCount { get; } = 2;

        public NetworkData GetTrainData() => IntToNetworkData(R.Next(4));

        public NetworkData GetTestData() => IntToNetworkData(internalVal++);

        public IEnumerable<NetworkData> GetAllTestData()
        {
            return new[] {0, 1, 2, 3}.Select(IntToNetworkData);
        }
        
        private NetworkData IntToNetworkData(int i)
        {
            var left = Convert.ToBoolean(i & 2);
            var right = Convert.ToBoolean(i & 1);
            var result = generator(left, right);
            var input = new List<double>
            {
                Convert.ToDouble(left),
                Convert.ToDouble(right)
            };
            var output = new List<double>()
            {
                Convert.ToDouble(!result),
                Convert.ToDouble(result)
            };
            return new NetworkData(input, output);
        }
    }
}
