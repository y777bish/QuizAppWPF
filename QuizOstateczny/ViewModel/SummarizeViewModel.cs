using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuizOstateczny.ViewModel
{
    class SummarizeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ICommand closeWindow;
        public ICommand CloseWindow
        {
            get
            {
                if (closeWindow == null)
                    closeWindow = new RelayCommand(

                     (o) =>
                     {
                         Application.Current.MainWindow.Close();
                     }
                    ,
                    (o) => true
                    );
                return closeWindow;
            }
        }
    }
}
