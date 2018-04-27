using System;

namespace NeuralWpf.Providers.Data
{
    public class LogicalOperatorComboBoxItem
    {
        public string Text { get; set; }
        public Func<bool, bool, bool> Generator { get; set; }
    }
}
