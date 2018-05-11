using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Neural.Core;
using Neural.Core.Helpers;
using Neural.Core.Providers;
using Neural.Mnist;
using NeuralWpf.Dto;
using NeuralWpf.Providers;

namespace NeuralWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Network Network => Driver.Network;

        public Network PublicNetwork => Network.Clone();

        private NetworkController Driver { get; }

        private ISettingsProvider SettingsProvider { get; }

        private IDataProvider DataProvider { get; set; }

        private IDataProvider GetDataProvider() => DataProvider;

        private MainVm ViewModel => (MainVm)DataContext;
        
        public MainWindow()
        {
            InitializeComponent();
            SettingsProvider = new SettingsProvider(ViewModel);
            ViewModel.Layers = new ObservableCollection<LayerSettings>(SettingsProvider.LayersSettings);

            DataProvider = new MnistDataProvider();
            Driver = new NetworkController(SettingsProvider, GetDataProvider);
            Driver.ReadyToRun += Driver_ReadyToRun;
            Driver.ReadyToFullTest += Driver_ReadyToFullTest;
        }

        private void Driver_ReadyToFullTest(object sender, EventArgs e)
        {
            FullTest(Network);
        }

        private async void FullTest_OnClick(object sender, EventArgs e) => await RunWithUiLock(() => FullTest(Network));

        private void FullTest(Network network)
        {
            var total = 0;
            var valid = 0;
            var mse = 0D;
            var crossEntropy = 0D;
            foreach (var testData in DataProvider.GetAllTestData())
            {
                var actual = network.Run(testData.Input);
                if (actual == null)
                {
                    return;
                }
                mse += CompareResults.Mse(testData.Output, actual);
                crossEntropy += CompareResults.CrossEntropy(testData.Output, actual);
                if (CompareResults.Validate(testData.Output, actual))
                {
                    valid++;
                }
                total++;
            }
            var classificationError = ((double)total - valid) / total;
            mse /= total;
            crossEntropy /= total;
            var newFullTestResult = new FullTestResultDto(classificationError, mse, crossEntropy);
            Application.Current.Dispatcher.Invoke(() => ViewModel.FullTestResults.Add(newFullTestResult));
        }

        private void Driver_ReadyToRun(object sender, EventArgs e) => RunWithTestData(Network);

        private async void Run_OnClick(object sender, EventArgs e) => await RunWithUiLock(() =>
        {
            var network = Network;
            for (int i = 0; i < 10; i++)
            {
                RunWithTestData(network);
            }
        });

        private void RunWithTestData(Network network)
        {
            var testData = DataProvider.GetTestData();
            var actual = Network.Run(testData.Input);
            if (actual == null)
            {
                return;
            }
            var newResult = new RunResultDto(testData, actual);
            Application.Current.Dispatcher.Invoke(() =>
            {
                ViewModel.RunResults.Add(newResult);
                RunResultsListView.ScrollIntoView(newResult);
            });
        }

        private async void Train_OnClick(object sender, RoutedEventArgs e) => await RunWithUiLock(() => Driver.Train());

        private void Stop_OnClick(object sender, RoutedEventArgs e) => Driver.Stop();

        private void DeleteLayer_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            ViewModel.Layers.Remove((LayerSettings)btn.DataContext);
        }

        private void AddLayer_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var prevLayer = (LayerSettings)btn.DataContext;
            var index = ViewModel.Layers.IndexOf(prevLayer) + 1;
            ViewModel.Layers.Insert(index, new LayerSettings { HasBias = true, NeuronsCount = prevLayer.NeuronsCount });
        }

        private async Task RunWithUiLock(Action action)
        {
            var watch = Stopwatch.StartNew();
            ViewModel.IsRunning = true;
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
