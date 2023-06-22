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

        private ObservableCollection<Quest> questList3;
        public ObservableCollection<Quest> QuestList3
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

        public QuestDatabaseConnection dbConnection { get; set; }

        public SolveThisViewModel()
        {
            QuestionContent = new ObservableCollection<Quest>();
            string dbPath = "C:\\Users\\barti\\Downloads\\db_quest.db";
            dbConnection = new QuestDatabaseConnection(dbPath);
            ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(1);
            /*QuizList2 = updatedQuizList;
            ListaLiczb = new ObservableCollection<int> { 1, 2, 3, 4 };*/
            /*SelectedNumber = 1;*/
        }

        private int quizTime2 = 180;
        public int QuizTime2
        {
            get => quizTime2;
            set
            {
                quizTime2 = value;
                OnPropertyChanged(nameof(QuizTime2));
            }
        }

        /*private string quizName2 = "";
        public string QuizName2
        {
            get => quizName2;
            set
            {
                quizName2 = value;
                OnPropertyChanged($"{nameof(QuizName2)}");
            }
        }*/

        private Quiz selectedQuiz2;
        public Quiz SelectedQuiz2
        {
            get => selectedQuiz2;
            set
            {
                selectedQuiz2 = value;
                OnPropertyChanged(nameof(SelectedQuiz2));
                if (selectedQuiz2 != null)
                {
                    ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(selectedQuiz2.Id);
                    QuestList3 = updatedQuestList;
                    /*QuizName2 = SelectedQuiz2.Name;*/
                    QuizTime2 = SelectedQuiz2.Czas;
                }
                else
                {
                    ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(-1);
                    QuestList3 = updatedQuestList;
                    /*QuizName2 = "";*/
                    QuizTime2 = 180;
                }

            }
        }

        private Quest selectedQuest2;
        public Quest SelectedQuest2
        {
            get => selectedQuest2;
            set
            {
                selectedQuest2 = value;
                OnPropertyChanged(nameof(SelectedQuest2));
                if (selectedQuest2 != null)
                {
                    Pytanie_2 = Szyfrowanko.Odszyfrowanie(SelectedQuest2.Tresc, "modelmvvm");
                    Odp1_2 = Szyfrowanko.Odszyfrowanie(SelectedQuest2.Odp_1, "modelmvvm");
                    Odp2_2 = Szyfrowanko.Odszyfrowanie(SelectedQuest2.Odp_2, "modelmvvm");
                    Odp3_2 = Szyfrowanko.Odszyfrowanie(SelectedQuest2.Odp_3, "modelmvvm");
                    Odp4_2 = Szyfrowanko.Odszyfrowanie(SelectedQuest2.Odp_4, "modelmvvm");
                    SelectedNumber2 = Szyfrowanko.DeszyfrNumer(selectedQuest2.Poprawna_odp);
                }
                else
                {
                    Pytanie_2 = "";
                    Odp1_2 = "";
                    Odp2_2 = "";
                    Odp3_2 = "";
                    Odp4_2 = "";
                    SelectedNumber2 = 1;
                }
            }
        }

        private int selectedNumber2;
        public int SelectedNumber2
        {
            get { return selectedNumber2; }
            set
            {
                selectedNumber2 = value;
                OnPropertyChanged(nameof(SelectedNumber2));
            }
        }

        private string pytanie_2 = "";
        public string Pytanie_2
        {
            get => pytanie_2;
            set
            {
                pytanie_2 = value;
                OnPropertyChanged(nameof(Pytanie_2));
            }
        }

        private string odp1_2 = "";
        public string Odp1_2
        {
            get => odp1_2;
            set
            {
                odp1_2 = value;
                OnPropertyChanged(nameof(Odp1_2));
            }
        }

        private string odp2_2 = "";
        public string Odp2_2
        {
            get => odp2_2;
            set
            {
                odp2_2 = value;
                OnPropertyChanged(nameof(Odp2_2));
            }
        }

        private string odp3_2 = "";
        public string Odp3_2
        {
            get => odp3_2;
            set
            {
                odp3_2 = value;
                OnPropertyChanged(nameof(Odp3_2));
            }
        }

        private string odp4_2 = "";
        public string Odp4_2
        {
            get => odp4_2;
            set
            {
                odp4_2 = value;
                OnPropertyChanged(nameof(Odp4_2));
            }
        }
    }
}
