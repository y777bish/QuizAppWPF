using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuizOstateczny.ViewModel;

namespace QuizOstateczny.ViewModel
{
    public abstract class MainViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateCommand { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
            /*NavigateCommand = new RelayCommand<string>(Navigate);*/
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Navigate(string viewName)
        {
            // Wywołaj odpowiednią metodę nawigacji w zależności od wartości viewName
            System.Windows.MessageBox.Show($"Wywołano polecenie z parametrem");
        }
    }

}
