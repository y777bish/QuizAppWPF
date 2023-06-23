using System.ComponentModel;
using System.Windows.Input;

namespace QuizOstateczny.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
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
