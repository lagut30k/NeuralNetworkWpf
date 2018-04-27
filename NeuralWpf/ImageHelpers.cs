using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Neural.Mnist;

namespace NeuralWpf
{
    public static class ImageHelpers
    {
        public static BitmapSource ToImage(byte[] array)
        {
            var inverted = array.Select(x => (byte)~x).ToArray();
            var width = 28;
            var height = 28;
            var dpiX = 96d;
            var dpiY = 96d;
            var pixelFormat = PixelFormats.Gray8;
            var bytesPerPixel = (pixelFormat.BitsPerPixel + 7) / 8;
            var stride = bytesPerPixel * width;

            var bitmap = BitmapSource.Create(width, height, dpiX, dpiY,
                pixelFormat, null, inverted, stride);
            return bitmap;
        }

        public static BitmapSource ToImage(List<double> doubles) => ToImage(doubles.ToBytes());
    }
}
