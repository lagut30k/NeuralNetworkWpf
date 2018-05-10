namespace NeuralWpf.Dto
{
    public class FullTestResultDto
    {
        public string ClassificationError { get; }

        public string Mse { get; }

        public string CrossEntropy { get; }

        public FullTestResultDto(double classificationError, double mse, double crossEntropy)
        {
            ClassificationError = DoubleToString(classificationError);
            Mse = DoubleToString(mse);
            CrossEntropy = DoubleToString(crossEntropy);
        }

        private static string DoubleToString(double d) => $"{(d * 100):F2} %";
    }
}
