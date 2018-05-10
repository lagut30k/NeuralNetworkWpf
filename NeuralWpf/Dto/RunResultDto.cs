using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Neural.Core.Providers;

namespace NeuralWpf.Dto
{
    public class RunResultDto
    {
        public RunResultDto(NetworkData testData, List<double> actual)
        {
            Expected = ListToLabel(testData.Output).ToString();
            Actual = ListToLabel(actual).ToString();
            ActualPercentage = $"{actual[ListToLabel(actual)] * 100:F1} %";
            IsRightResult = Actual == Expected;
            Application.Current.Dispatcher.Invoke(() => ImageBitmap = ImageHelpers.ToImage(testData.Input));
        }

        public string Expected { get; }

        public string Actual { get; }

        public BitmapSource ImageBitmap { get; private set; }

        public string ActualPercentage { get; }

        public bool IsRightResult { get; }

        private static int ListToLabel(List<double> list)
        {
            var m = list.Max();
            return list.IndexOf(m);
        }
    }
}
