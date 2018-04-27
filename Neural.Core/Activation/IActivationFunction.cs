using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural.Core.Activation
{
    public interface IActivationFunction
    {
        double CalcValue(double x);

        double CalcGradByValue(double value);

        double GetInitWeight(int inputNeurons, int outputNeurons);
    }
}
