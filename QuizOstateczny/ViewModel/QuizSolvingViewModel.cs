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
        private Quiz selectedQuiz2;
        public Quiz SelectedQuiz2
        {
            get => selectedQuiz2;
            set
            {
                selectedQuiz2 = value;
                OnPropertyChanged(nameof(SelectedQuiz2));
            }
        }

        private ObservableCollection<Quest> questList2;
        public ObservableCollection<Quest> QuestList2
        {
            get { return questList2; }
            set
            {
                questList2 = value;
                OnPropertyChanged(nameof(QuestList2));
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

        private ObservableCollection<int> listaLiczb2;
        public ObservableCollection<int> ListaLiczb2
        {
            get { return listaLiczb2; }
            set
            {
                listaLiczb2 = value;
                OnPropertyChanged(nameof(ListaLiczb2));
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

        public QuestDatabaseConnection dbConnection2 { get; set; }

        public QuizSolvingViewModel()
        {
            QuestList2 = new ObservableCollection<Quest>();
            QuizList2 = new ObservableCollection<Quiz>();
            string dbPath = "C:\\Users\\barti\\Downloads\\db_quest.db";
            dbConnection2 = new QuestDatabaseConnection(dbPath);
            ObservableCollection<Quiz> updatedQuizList = dbConnection2.GetQuizList();
            QuizList2 = updatedQuizList;
            ListaLiczb2 = new ObservableCollection<int> { 1, 2, 3, 4 };
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
                                WybierzQuiz();
                                frame.Navigate(new SolveThis());
                            }
                        }
                        ,
                        (o) => selectedQuiz2 != null
                        );
                return zacznijQuiz;
            }
        }

        public event EventHandler<QuizSelectedEventArgs> QuizSelected;

        private void OnQuizSelected()
        {
            QuizSelected?.Invoke(this, new QuizSelectedEventArgs(selectedQuiz2.Id));
        }

        public void WybierzQuiz()
        {
            // Logika wyboru quizu...

            // Publikowanie zdarzenia
            OnQuizSelected();
        }
    }
}
