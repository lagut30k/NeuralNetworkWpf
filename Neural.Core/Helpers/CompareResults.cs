using System;
using System.Collections.Generic;
using System.Linq;

namespace Neural.Core.Helpers
{
    public static class CompareResults
    {
        public static bool Validate(List<double> expected, List<double> actual)
        {
            return MaxIndex(expected) == MaxIndex(actual);
        }

        public static double Mse(List<double> expected, List<double> actual)
        {
            return expected.Zip(actual, (e, a) => e - a).Sum(x => x * x) / expected.Count;
        }

        public static double CrossEntropy(List<double> expected, List<double> actual)
        {
            return expected.Zip(actual, (e, a) => -e * Math.Log(a + 1e-12)).Sum() / expected.Count;
        }

        private static int MaxIndex(List<double> list) => list.IndexOf(list.Max());
    }
}
