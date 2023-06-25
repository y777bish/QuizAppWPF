using QuizOstateczny.Model;
using QuizOstateczny.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;

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


        private List<Answer> answerList;
        public List<Answer> AnswerList
        {
            get => answerList;
            set
            {
                answerList = value;
                OnPropertyChanged(nameof(AnswerList));
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
            QuizTime3 = dbConnection3.FindQuiz(FileService.ReadQuizIDFromFile()).Czas;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            AnswerList = new List<Answer>();
            foreach (Quest quest in QuestList3)
            {
                Answer answer = new Answer();
                answer.Answer1 = false;
                answer.Answer2 = false;
                answer.Answer3 = false;
                answer.Answer4 = false;

                AnswerList.Add(answer);
            }

            Sync(NrPytania - 1);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            QuizTime3 -= 1;
            if (quizTime3 <= 0)
            {
                timer.Stop();
                ZakonczenieQuizu();
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

                Ans1 = AnswerList[nrQuest].Answer1;
                Ans2 = AnswerList[nrQuest].Answer2;
                Ans3 = AnswerList[nrQuest].Answer3;
                Ans4 = AnswerList[nrQuest].Answer4;
            }
            else
            {
                Pytanie_3 = string.Empty;
                Odp1_3 = string.Empty;
                Odp2_3 = string.Empty;
                Odp3_3 = string.Empty;
                Odp4_3= string.Empty;
                Ans1 = false;
                Ans2 = false;
                Ans3 = false;
                Ans4 = false;
            }
        }

        public void AktualizacjaOdp(int pytanie, int ktory)
        {
            AnswerList[pytanie - 1].Answer1 = false;
            AnswerList[pytanie - 1].Answer2 = false;
            AnswerList[pytanie - 1].Answer3 = false;
            AnswerList[pytanie - 1].Answer4 = false;

            switch (ktory)
            {
                case 1:
                    AnswerList[pytanie - 1].Answer1 = true;
                    break;
                case 2:
                    AnswerList[pytanie - 1].Answer2 = true;
                    break;
                case 3:
                    AnswerList[pytanie - 1].Answer3 = true;
                    break;
                case 4:
                    AnswerList[pytanie - 1].Answer4 = true;
                    break;
            }
        }

        public void ZakonczenieQuizu()
        {
            int iloscPkt = 0;

            for (int i = 0; i < QuestList3.Count; i++)
            {
                if (AnswerList[i].Answer1)
                    if (Szyfrowanko.DeszyfrNumer(QuestList3[i].Poprawna_odp) == 1) iloscPkt += 1;
                if (AnswerList[i].Answer2)
                    if (Szyfrowanko.DeszyfrNumer(QuestList3[i].Poprawna_odp) == 2) iloscPkt += 1;
                if (AnswerList[i].Answer3)
                    if (Szyfrowanko.DeszyfrNumer(QuestList3[i].Poprawna_odp) == 3) iloscPkt += 1;
                if (AnswerList[i].Answer4)
                    if (Szyfrowanko.DeszyfrNumer(QuestList3[i].Poprawna_odp) == 4) iloscPkt += 1;
            }

            FileService.SavePtsToFile(iloscPkt);

            Frame frame = Application.Current.MainWindow.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new QuizWynik());
                
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

        private bool ans1;
        public bool Ans1
        {
            get { return ans1; } set
            {
                ans1 = value;
                if (ans1)
                {
                    Ans2 = false;
                    Ans3 = false;
                    Ans4 = false;
                    AktualizacjaOdp(NrPytania, 1);
                }
                else
                {
                    AnswerList[NrPytania - 1].Answer1 = false;
                }
                OnPropertyChanged(nameof(Ans1));
            }
        }

        private bool ans2;
        public bool Ans2
        {
            get { return ans2; }
            set
            {
                ans2 = value;
                if (ans2)
                {
                    Ans1 = false;
                    Ans3 = false;
                    Ans4 = false;
                    AktualizacjaOdp(NrPytania, 2);
                }
                else
                {
                    AnswerList[NrPytania - 1].Answer2 = false;
                }
                OnPropertyChanged(nameof(Ans2));
            }
        }

        private bool ans3;
        public bool Ans3
        {
            get { return ans3; }
            set
            {
                ans3 = value;
                if (ans3)
                {
                    Ans2 = false;
                    Ans1 = false;
                    Ans4 = false;
                    AktualizacjaOdp(NrPytania, 3);
                }
                else
                {
                    AnswerList[NrPytania - 1].Answer3 = false;
                }
                OnPropertyChanged(nameof(Ans3));
            }
        }

        private bool ans4;
        public bool Ans4
        {
            get { return ans4; }
            set
            {
                ans4 = value;
                if (ans4)
                {
                    Ans2 = false;
                    Ans3 = false;
                    Ans1 = false;
                    AktualizacjaOdp(NrPytania, 4);
                }
                else
                {
                    AnswerList[NrPytania - 1].Answer4 = false;
                }
                OnPropertyChanged(nameof(Ans4));
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
                            if (NrPytania == questList3.Count)
                            {
                                ZakonczenieQuizu();
                            }
                            NrPytania += 1;
                            if (NrPytania <= questList3.Count)
                            {
                                Sync(NrPytania - 1);
                                if (NrPytania == questList3.Count)
                                {
                                    PrzyciskDalej = "Zakończ";
                                }
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
