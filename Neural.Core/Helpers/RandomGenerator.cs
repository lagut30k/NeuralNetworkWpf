using System;
using System.Linq;
using System.Security.Cryptography;

namespace Neural.Core.Helpers
{
    public class RandomGenerator
    {
        private static readonly RNGCryptoServiceProvider Rng = new RNGCryptoServiceProvider();

        public static bool NextBool()
        {
            var b = new byte[1];
            Rng.GetBytes(b);
            return Convert.ToBoolean(b[0] & 0b1);
        }

        public static bool NextBool(double probability)
        {
            return NextDouble() < probability;
        }

        public static double NextDouble()
        {
            var bytes = new byte[8];
            Rng.GetBytes(bytes);
            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            return ul / (double)(1UL << 53);
        }

        public static double Uniform(double variance) => (NextDouble() - 0.5) * Math.Sqrt(12 * variance);

        public static double Gauss(double variance) => Enumerable.Repeat(0, 12).Sum(_ => NextDouble() - 0.5) * Math.Sqrt(variance);
    }
}
