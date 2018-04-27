using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Neural.Mnist
{
    public class MnistData
    {
        private const string TrainImagesName = "train-images.idx3-ubyte";
        private const string TrainLabelsName = "train-labels.idx1-ubyte";
        private const string TestImagesName = "t10k-images.idx3-ubyte";
        private const string TestLabelsName = "t10k-labels.idx1-ubyte";
        private const int Width = 28;
        private const int Height = 28;

        public static List<byte[]> TrainImages = LoadImages(TrainImagesName);
        public static List<byte> TrainLabels = LoadLabels(TrainLabelsName);

        public static List<byte[]> TestImages = LoadImages(TestImagesName);
        public static List<byte> TestLabels = LoadLabels(TestLabelsName);

        private static List<byte> LoadLabels(string resourceName)
        {
            var byteArray = LoadResourse(resourceName);
            var magicNumber = byteArray.ToInt32(0);
            var labelsCount = byteArray.ToInt32(4);
            var labels = new List<byte>(labelsCount);
            var labelIndex = 8;
            for (int i = 0; i < labelsCount; i++)
            {
                labels.Add(byteArray[labelIndex++]);
            }
            return labels;
        }

        private static List<byte[]> LoadImages(string resourceName)
        {
            var byteArray = LoadResourse(resourceName);
            var magicNumber = byteArray.ToInt32(0);
            var imageCount = byteArray.ToInt32(4);
            var height = byteArray.ToInt32(8);
            var width = byteArray.ToInt32(12);
            var byteImages = new List<byte[]>(imageCount);
            var pointIndex = 16;
            var imageSize = height * width;
            for (int imageIndex = 0; imageIndex < imageCount; imageIndex++)
            {
                var byteImage = new byte[height * width];
                for (int i = 0; i < imageSize; i++, pointIndex++)
                {
                    byteImage[i] = byteArray[pointIndex];
                }
                byteImages.Add(byteImage);
            }
            return byteImages;
        }

        private static byte[] LoadResourse(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream($"Neural.Mnist.{resourceName}"))
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
