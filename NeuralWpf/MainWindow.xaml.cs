using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Neural.Core;
using Neural.Core.Providers;
using Neural.Mnist;
using NeuralWpf.Providers;
//using NeuralWpf.Mnist;
//using NeuralWpf.Providers;
//using NeuralWpf.Providers.Data;

namespace NeuralWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Network Network => Driver.Network;

        private NetworkController Driver { get; }

        private ISettingsProvider SettingsProvider { get; }

        private IDataProvider DataProvider { get; set; }

        private IDataProvider GetDataProvider() => DataProvider;

        private MainVm ViewModel => (MainVm) DataContext;

        public MainWindow()
        {
            InitializeComponent();
            SettingsProvider = new SettingsProvider();
            DataProvider = new MnistDataProvider();
            Driver = new NetworkController(SettingsProvider, GetDataProvider);
        }

        private async void Train_OnClick(object sender, RoutedEventArgs e) => await RunWithUiLock(() => Driver.Train());

        private void Stop_OnClick(object sender, RoutedEventArgs e) => Driver.Stop();

        private async Task RunWithUiLock(Action action)
        {
            var watch = Stopwatch.StartNew();
            ViewModel.IsRunning = true;
            var myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = 1;
            myLine.X2 = 50;
            myLine.Y1 = 1;
            myLine.Y2 = 50;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            //MyGrid.Children.Add(myLine);
            //Canvas.Children.Add(myLine);
            //Canvas.Children.Add()
            //Image.Source = MnistData.TestImages[0].ToBitmapSource();
            Image.Source = ImageHelpers.ToImage(MnistData.TestImages[0]);
            try
            {
                await Task.Run(action);
            }
            finally
            {
                ViewModel.IsRunning = false;
            }
            watch.Stop();
        }
    }
}
