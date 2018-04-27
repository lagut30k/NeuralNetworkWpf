namespace Neural.Core
{
    public class LayerSettings
    {
        public int NeuronsCount { get; set; }

        public bool HasBias { get; set; }

        public void Deconstruct(out int neuronsCount, out bool hasBias)
        {
            neuronsCount = NeuronsCount;
            hasBias = HasBias;
        }
    }
}
