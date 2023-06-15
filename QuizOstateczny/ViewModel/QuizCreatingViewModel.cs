using QuizOstateczny.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace QuizOstateczny.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Quest> QuestList { get; set; }
        public ObservableCollection<Quiz> QuizList { get; set; }

        public ViewModel()
        {
            QuestList = new ObservableCollection<Quest>();
            QuizList = new ObservableCollection<Quiz>();
        }

        private ICommand dodajQuest;
        public ICommand DodajQuest
        {
            get
            {
                if (dodajQuest == null)
                    dodajQuest = new RelayCommand(
                        (o) =>
                        {
                            Quest newQuest = new Quest()
                            {
                                Tresc = pytanie,
                                Odp_1 = odp1,
                                Odp_2 = odp2,
                                Odp_3 = odp3,
                                Odp_4 = odp4,

                            };

                            QuestList.Add(newQuest);
                            ClearNewQuestFields();
                        },
                        (o) => !string.IsNullOrEmpty(Pytanie)
                        && !string.IsNullOrEmpty(Odp1)
                        && !string.IsNullOrEmpty(Odp2)
                        && !string.IsNullOrEmpty(Odp3)
                        && !string.IsNullOrEmpty(Odp4)
                    );
                return dodajQuest;
            }
        }

        private ICommand dodajQuiz;
        public ICommand DodajQuiz
        {
            get
            {
                if (dodajQuiz == null)
                    dodajQuiz = new RelayCommand(
                        (o) =>
                        {
                            Quiz newQuiz = new Quiz()
                            {
                                Name = quizName
                            };

                            QuizList.Add(newQuiz);
                        },
                        (o) => !string.IsNullOrEmpty(QuizName)
                    );
                return dodajQuiz;
            }
        }

        private void ClearNewQuestFields()
        {
            Pytanie = "";
            Odp1 = "";
            Odp2 = "";
            Odp3 = "";
            Odp4 = "";
        }

        private string pytanie = "";
        public string Pytanie
        {
            get => pytanie;
            set
            {
                pytanie = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Pytanie));
            }
        }

        private string odp1 = "";
        public string Odp1
        {
            get => odp1;
            set
            {
                odp1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Odp1));
            }
        }

        private string odp2 = "";
        public string Odp2
        {
            get => odp2;
            set
            {
                odp2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Odp2));
            }
        }

        private string odp3 = "";
        public string Odp3
        {
            get => odp3;
            set
            {
                odp3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Odp3));
            }
        }

        private string odp4 = "";
        public string Odp4
        {
            get => odp4;
            set
            {
                odp4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Odp4));
            }
        }

        private int quizTime = 180;
        public int QuizTime
        {
            get => quizTime;
            set
            {
                quizTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(quizTime)));
            }
        }

        private string quizName = "";
        public string QuizName
        {
            get => quizName;
            set
            {
                quizName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(QuizName));
            }
        }

        private Quiz selectedQuiz;
        public Quiz SelectedQuiz
        {
            get => selectedQuiz;
            set
            {
                selectedQuiz = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"Selected Quiz"));
            }
        }

        private Quest selectedQuest;
        public Quest SelectedQuest
        {
            get => selectedQuest;
            set
            {
                selectedQuest = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"{nameof(selectedQuest)}"));
            }
        }
    }
}
