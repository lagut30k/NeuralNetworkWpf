using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Neural.Core;
using NeuralWpf.Annotations;
using NeuralWpf.Dto;

namespace NeuralWpf
{
    public class MainVm : INotifyPropertyChanged
    {
        private bool isRunning;
        private ObservableCollection<LayerSettings> layers;
        private ObservableCollection<RunResultDto> runResults = new ObservableCollection<RunResultDto>();
        private ObservableCollection<FullTestResultDto> fullTestResults = new ObservableCollection<FullTestResultDto>();
        private double learningRate = 0.7;
        private double moment = 0.9;
        private double dropoutProbability = 0.12;
        private int trainLoops = 100000;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsStopped => !IsRunning;

        public bool IsRunning
        {
            get => isRunning;
            set
            {
                isRunning = value;
                OnPropertyChanged(nameof(IsStopped));
                OnPropertyChanged(nameof(IsRunning));
            }
        }
        
        public ObservableCollection<LayerSettings> Layers
        {
            get => layers;
            set
            {
                layers = value;
                OnPropertyChanged(nameof(Layers));
            }
        }

        public ObservableCollection<RunResultDto> RunResults
        {
            get => runResults;
            set
            {
                runResults = value;
                OnPropertyChanged(nameof(RunResults));
            }
        }
        public ObservableCollection<FullTestResultDto> FullTestResults
        {
            get => fullTestResults;
            set
            {
                fullTestResults = value;
                OnPropertyChanged(nameof(FullTestResults));
            }
        }

        public double LearningRate
        {
            get => learningRate;
            set
            {
                learningRate = value;
                OnPropertyChanged(nameof(LearningRate));
            }
        }

        public double Moment
        {
            get => moment;
            set
            {
                moment = value;
                OnPropertyChanged(nameof(Moment));
            }
        }

        public double DropoutProbability
        {
            get => dropoutProbability;
            set
            {
                dropoutProbability = value;
                OnPropertyChanged(nameof(DropoutProbability));
            }
        }

        public int TrainLoops
        {
            get => trainLoops;
            set
            {
                trainLoops = value;
                OnPropertyChanged(nameof(TrainLoops));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
