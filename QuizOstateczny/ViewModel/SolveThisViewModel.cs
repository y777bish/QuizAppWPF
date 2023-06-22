using QuizOstateczny.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
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

        private List<Quest> questList3;
        public List<Quest> QuestList3
        {
            get { return questList3; }
            set
            {
                questList3 = value;
                OnPropertyChanged(nameof(QuestList3));
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public QuestDatabaseConnection dbConnection3 { get; set; }

        Quiz quiz = new Quiz();

        public event EventHandler<QuizSelectedEventArgs> QuizSelected;

        public SolveThisViewModel()
        {
            
            QuizSolvingViewModel quizSolvingViewModel = new QuizSolvingViewModel();
            QuizSelected += QuizSolvingViewModel_QuizSelected;

            questList3 = new List<Quest>();
            
            string dbPath = "C:\\Users\\barti\\Downloads\\db_quest.db";
            dbConnection3 = new QuestDatabaseConnection(dbPath);
            //QuestList3 = dbConnection3.GetQuestList3(selectedQuiz3.Id);
            //Sync(1);
        }
        public void Sync(int nrQuest)
        {
            if (nrQuest >= 0 && nrQuest < questList3.Count)
            {
                Pytanie_3 = questList3[nrQuest].Tresc;
            }
            else
            {
                Pytanie_3 = string.Empty;
            }
        }

        private int quizTime3 = 180;
        public int QuizTime3
        {
            get => quizTime3;
            set
            {
                quizTime3 = value;
                OnPropertyChanged(nameof(QuizTime3));
            }
        }

        private Quiz selectedQuiz3;
        public Quiz SelectedQuiz3
        {
            get => selectedQuiz3;
            set
            {
                selectedQuiz3 = value;
                OnPropertyChanged(nameof(SelectedQuiz3));
            }
        }

        private int selectedNumber3;
        public int SelectedNumber3
        {
            get { return selectedNumber3; }
            set
            {
                selectedNumber3 = value;
                OnPropertyChanged(nameof(SelectedNumber3));
            }
        }

        private string pytanie_3 = "";
        public string Pytanie_3
        {
            get => pytanie_3;
            set
            {
                pytanie_3 = value;
                OnPropertyChanged(nameof(Pytanie_3));
            }
        }

        private string odp1_3 = "";
        public string Odp1_3
        {
            get => odp1_3;
            set
            {
                odp1_3 = value;
                OnPropertyChanged(nameof(Odp1_3));
            }
        }

        private string odp2_3 = "";
        public string Odp2_3
        {
            get => odp2_3;
            set
            {
                odp2_3 = value;
                OnPropertyChanged(nameof(Odp2_3));
            }
        }

        private string odp3_3 = "";
        public string Odp3_3
        {
            get => odp3_3;
            set
            {
                odp3_3 = value;
                OnPropertyChanged(nameof(Odp3_3));
            }
        }

        private string odp4_3 = "";
        public string Odp4_3
        {
            get => odp4_3;
            set
            {
                odp4_3 = value;
                OnPropertyChanged(nameof(Odp4_3));
            }
        }

        private void QuizSolvingViewModel_QuizSelected(object sender, QuizSelectedEventArgs e)
        {
            // Otrzymanie przekazanej wartości wybranyQuiz
            Quiz wybranyQuiz = dbConnection3.FindQuiz(e.WybranyQuiz);

            SelectedQuiz3 = wybranyQuiz;
            Pytanie_3 = "ggg";

            QuizSelected?.Invoke(this, e);
        }
    }
}
