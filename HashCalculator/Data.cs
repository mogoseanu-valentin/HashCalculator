using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCalculator
{
    public class Data : INotifyPropertyChanged
    {
        private ObservableCollection<HashResult>? _hashResult = new ObservableCollection<HashResult>();
        public ObservableCollection<HashResult>? HashResults
        {
            get => _hashResult;
            set
            {
                _hashResult = value;
                OnPropertyChanged(nameof(HashResults));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadData()
        {
            
        }

        public void SaveData()
        {

        }
    }
}
