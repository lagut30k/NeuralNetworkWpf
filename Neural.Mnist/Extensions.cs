using System;
using System.Collections.Generic;
using System.Linq;
//using System.Drawing;
//using System.Drawing.Imaging;

//using System.Runtime.InteropServices;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using Color = System.Drawing.Color;
//using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Neural.Mnist
{
    public static class Extensions
    {
        public static int ToInt32(this byte[] byteArray, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                var reversed = byteArray.Skip(startIndex).Take(4).Reverse().ToArray();
                return BitConverter.ToInt32(reversed, 0);
            }
            return BitConverter.ToInt32(byteArray, startIndex);
        }

        public static List<double> ToDoubles(this byte[] byteArray) => byteArray.Select(x => x / 255D).ToList();

        public static byte[] ToBytes(this List<double> doubles) => doubles.Select(x => (byte)(x * 255)).ToArray();
    }
}
