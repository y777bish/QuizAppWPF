using QuizOstateczny.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
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

        public event EventHandler<QuizSelectedEventArgs> QuizSelected;

        public int NrPytania = 1;

        private string nrPytania2 = "Pytanie";
        public string NrPytania2
        {
            get => nrPytania2;
            set
            {
                nrPytania2 = value;
                OnPropertyChanged(nameof(NrPytania2));
            }
        }

        public SolveThisViewModel()
        {

            questList3 = new List<Quest>();
            
            //string dbPath = "C:\\Users\\barti\\Downloads\\db_quest.db";
            string dbPath = "C:\\Users\\lukas\\OneDrive\\Desktop\\db_quest.db";
            dbConnection3 = new QuestDatabaseConnection(dbPath);
            QuestList3 = dbConnection3.GetQuestList3(FileService.ReadQuizIDFromFile());
            Sync(NrPytania - 1);
            QuizTime3 = dbConnection3.FindQuiz(FileService.ReadQuizIDFromFile()).Czas;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            QuizTime3 -= 1;
            if (quizTime3 <= 0)
            {
                timer.Stop();
            }
        }

        public void Sync(int nrQuest)
        {
            if (nrQuest >= 0 && nrQuest < questList3.Count)
            {
                Pytanie_3 = Szyfrowanko.Odszyfrowanie(questList3[nrQuest].Tresc, "modelmvvm");
                Odp1_3 = Szyfrowanko.Odszyfrowanie(questList3[nrQuest].Odp_1, "modelmvvm");
                Odp2_3 = Szyfrowanko.Odszyfrowanie(questList3[nrQuest].Odp_2, "modelmvvm");
                Odp3_3 = Szyfrowanko.Odszyfrowanie(questList3[nrQuest].Odp_3, "modelmvvm");
                Odp4_3 = Szyfrowanko.Odszyfrowanie(questList3[nrQuest].Odp_4, "modelmvvm");
                NrPytania2 = String.Concat("Pytanie nr ", nrQuest + 1);
            }
            else
            {
                Pytanie_3 = string.Empty;
                Odp1_3 = string.Empty;
                Odp2_3 = string.Empty;
                Odp3_3 = string.Empty;
                Odp4_3= string.Empty;
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

        private DispatcherTimer timer;

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

        private string przyciskDalej = "Następne pytanie";
        public string PrzyciskDalej
        {
            get => przyciskDalej;
            set
            {
                przyciskDalej = value;
                OnPropertyChanged(nameof(przyciskDalej));
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

        private ICommand nextQuestion;
        public ICommand NextQuestion
        {
            get
            {
                if (nextQuestion == null)
                {
                    nextQuestion = new RelayCommand(
                        (o) =>
                        {
                            NrPytania += 1;
                            if (NrPytania <= questList3.Count)
                            {
                                Sync(NrPytania - 1);
                                if (NrPytania == questList3.Count)
                                {
                                    PrzyciskDalej = "Zakończ";
                                }
                            }
                            else
                            {

                            }
                        },
                        (o) => true);
                }
                return nextQuestion;
            }
        }

        private ICommand previousQuestion;
        public ICommand PreviousQuestion
        {
            get
            {
                if (previousQuestion == null)
                {
                    previousQuestion = new RelayCommand(
                        (o) =>
                        {
                            NrPytania -= 1;
                            Sync(NrPytania - 1);
                            if (NrPytania == QuestList3.Count - 1)
                            {
                                PrzyciskDalej = "Następne pytanie";
                            }
                        },
                        (o) => NrPytania != 1);
                }
                return previousQuestion;
            }
        }
    }
}
