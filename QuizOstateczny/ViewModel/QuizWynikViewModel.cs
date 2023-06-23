using QuizOstateczny.Model;
using QuizOstateczny.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace QuizOstateczny.ViewModel
{
    public class QuizWynikViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public QuestDatabaseConnection dbConnection4 { get; set; }

        private ObservableCollection<Quiz> quizList4;
        public ObservableCollection<Quiz> QuizList4
        {
            get { return quizList4; }
            set
            {
                quizList4 = value;
                OnPropertyChanged(nameof(QuizList4));
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public QuizWynikViewModel()
        {
            //string dbPath = "C:\\Users\\barti\\Downloads\\db_quest.db";
            string dbPath = "C:\\Users\\lukas\\OneDrive\\Desktop\\db_quest.db";
            dbConnection4 = new QuestDatabaseConnection(dbPath);

            ObservableCollection<Quiz> updatedQuizList = dbConnection4.GetQuizList();
            QuizList4 = updatedQuizList;

            int Pts = FileService.ReadQuizIDFromFile();
            TotalPts = Pts + "/" + QuizList4.Count;
        }

        private Quiz selectedQuiz4;
        public Quiz SelectedQuiz4
        {
            get => selectedQuiz4;
            set
            {
                selectedQuiz4 = value;
                OnPropertyChanged(nameof(SelectedQuiz4));
            }
        }

        private string totalPts;
        public string TotalPts
        {
            get => totalPts;
            set
            {
                totalPts = value;
                OnPropertyChanged(nameof(TotalPts));
            }
        }

        private ICommand zacznijQuiz4;
        public ICommand ZacznijQuiz4
        {
            get
            {
                if (zacznijQuiz4 == null)
                    zacznijQuiz4 = new RelayCommand(
                        (o) =>
                        {
                            Frame frame = Application.Current.MainWindow.FindName("MainFrame") as Frame;

                            if (frame != null)
                            {
                                FileService.SaveToFile(selectedQuiz4.Id);
                                frame.Navigate(new SolveThis());
                            }
                        }
                        ,
                        (o) => selectedQuiz4 != null
                        );
                return zacznijQuiz4;
            }
        }
    }
}
