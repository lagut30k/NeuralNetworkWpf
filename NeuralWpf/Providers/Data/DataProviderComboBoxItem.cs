using System;
using Neural.Core.Providers;

namespace NeuralWpf.Providers.Data
{
    public class DataProviderComboBoxItem
    {
        public string Text { get; set; }

        public Func<IDataProvider> Factory { get; set; }
    }
}
