using QuizOstateczny.Model;
using QuizOstateczny.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizOstateczny.ViewModel
{
    class QuizSolvingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        /*public static readonly Model.QuestDatabaseConnection model = new Model.QuestDatabaseConnection();*/

        // Nie jestem tego pewien, możliwe, że coś podobnego zrobił Łukasz
        private static int selectedQuiz = 0;
        public static int SelectedQuiz
        {
            get => selectedQuiz;
            set
            {
                selectedQuiz = value;
            }
        }

        private ObservableCollection<Quest> questList;
        public ObservableCollection<Quest> QuestList
        {
            get { return questList; }
            set
            {
                questList = value;
                OnPropertyChanged(nameof(QuestList));
            }
        }

        private ObservableCollection<Quiz> quizList2;
        public ObservableCollection<Quiz> QuizList2
        {
            get { return quizList2; }
            set
            {
                quizList2 = value;
                OnPropertyChanged(nameof(QuizList2));
            }
        }

        private ObservableCollection<int> listaLiczb;
        public ObservableCollection<int> ListaLiczb
        {
            get { return listaLiczb; }
            set
            {
                listaLiczb = value;
                OnPropertyChanged(nameof(ListaLiczb));
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private Visibility quizListVisibility;
        public Visibility QuizListVisibility
        {
            get { return quizListVisibility; }
            set
            {
                quizListVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuizListVisibility)));
            }
        }
        private Visibility startQuizVisibility;
        public Visibility StartQuizVisibility
        {
            get { return startQuizVisibility; }
            set
            {
                startQuizVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StartQuizVisibility)));
            }
        }

        public QuestDatabaseConnection dbConnection { get; set; }

        public QuizSolvingViewModel()
        {
            QuestList = new ObservableCollection<Quest>();
            QuizList2 = new ObservableCollection<Quiz>();
            string dbPath = "C:\\Users\\barti\\Downloads\\db_quest.db";
            dbConnection = new QuestDatabaseConnection(dbPath);
            ObservableCollection<Quiz> updatedQuizList = dbConnection.GetQuizList();
            QuizList2 = updatedQuizList;
            ListaLiczb = new ObservableCollection<int> { 1, 2, 3, 4 };
            /*SelectedNumber = 1;*/
        }

        private ICommand zacznijQuiz;
        public ICommand ZacznijQuiz
        {
            get
            {
                if (zacznijQuiz == null)
                    zacznijQuiz = new RelayCommand(
                        (o) =>
                        {
                            Frame frame = Application.Current.MainWindow.FindName("MainFrame") as Frame;

                            if (frame != null)
                            {
                                QuizListVisibility = Visibility.Collapsed;
                                StartQuizVisibility = Visibility.Collapsed;
                                frame.Navigate(new SolveThis());
                            }
                        }
                        ,
                        (o) => selectedQuiz >-1
                        );
                return zacznijQuiz;
            }
        }
    }
}
