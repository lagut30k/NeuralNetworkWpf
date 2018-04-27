using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NeuralWpf.Annotations;

namespace NeuralWpf
{
    public class MainVm: INotifyPropertyChanged
    {
        private bool isRunning;
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
