using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neural.Core.Helpers;

namespace Neural.Core.Activation
{
    public class Sigmoid : IActivationFunction
    {
        public double CalcValue(double x) => 1 / (1 + Math.Exp(-x));

        public double CalcGradByValue(double value) => value * (1 - value);

        public double GetInitWeight(int inputNeurons, int outputNeurons) => RandomGenerator.Uniform(2D / (inputNeurons + outputNeurons));
    }
}
