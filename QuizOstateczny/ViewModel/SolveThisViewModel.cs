using QuizOstateczny.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuizOstateczny.ViewModel
{
    public class SolveThisViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Quest> questionContent;
        public ObservableCollection<Quest> QuestionContent
        {
            get { return questionContent; }
            set
            {
                questionContent = value;
                OnPropertyChanged(nameof(QuestionContent));
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public SolveThisViewModel()
        {
            QuestionContent = new ObservableCollection<Quest>();
        }

        /*private ICommand check;
        public ICommand Check
        {
            get
            {
                if (check == null)
                    check = new RelayCommand(

                     (o) =>
                     {

                     }
                    ,
                    (o) => !model.currentQuiz.ShowCorrectAnswers
                    );
                return check;
            }
        }*/
    }
}
