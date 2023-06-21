using QuizOstateczny.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace QuizOstateczny.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

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

        private ObservableCollection<Quiz> quizList;
        public ObservableCollection<Quiz> QuizList
        {
            get { return quizList; }
            set
            {
                quizList = value;
                OnPropertyChanged(nameof(QuizList));
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

        public QuestDatabaseConnection dbConnection { get; set; }

        public ViewModel()
        {
            QuestList = new ObservableCollection<Quest>();
            QuizList = new ObservableCollection<Quiz>();
            string dbPath = "C:\\Users\\barti\\Downloads\\db_quest.db";
            dbConnection = new QuestDatabaseConnection(dbPath);
            ObservableCollection<Quiz> updatedQuizList = dbConnection.GetQuizList();
            QuizList = updatedQuizList;
            ListaLiczb = new ObservableCollection<int>{1, 2, 3, 4};
            SelectedNumber = 1;
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
                                Poprawna_odp = Szyfrowanko.SzyfrNumer(SelectedNumber),
                                Quiz = SelectedQuiz.Id
                            };

                            QuestBaza questBaza = new QuestBaza();
                            questBaza.tresc = Szyfrowanko.Szyfrowanie(newQuest.Tresc, "modelmvvm");
                            questBaza.odp1 = Szyfrowanko.Szyfrowanie(newQuest.Odp_1, "modelmvvm");
                            questBaza.odp2 = Szyfrowanko.Szyfrowanie(newQuest.Odp_2, "modelmvvm");
                            questBaza.odp3 = Szyfrowanko.Szyfrowanie(newQuest.Odp_3, "modelmvvm");
                            questBaza.odp4 = Szyfrowanko.Szyfrowanie(newQuest.Odp_4, "modelmvvm");
                            questBaza.poprawna_odp = newQuest.Poprawna_odp;
                            questBaza.quiz = newQuest.Quiz;

                            dbConnection.AddQuest(questBaza);
                            ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(selectedQuiz.Id);
                            QuestList = updatedQuestList;
                            ClearNewQuestFields();
                        },
                        (o) => !string.IsNullOrEmpty(Pytanie)
                        && !string.IsNullOrEmpty(Odp1)
                        && !string.IsNullOrEmpty(Odp2)
                        && !string.IsNullOrEmpty(Odp3)
                        && !string.IsNullOrEmpty(Odp4)
                        && SelectedQuiz != null
                        && SelectedNumber != null
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
                                Name = quizName,
                                Czas = quizTime
                            };
                            
                            QuizBaza quizBaza = new QuizBaza();
                            quizBaza.nazwa = newQuiz.Name;
                            quizBaza.czas = newQuiz.Czas;

                            dbConnection.AddQuiz(quizBaza);
                            ObservableCollection<Quiz> updatedQuizList = dbConnection.GetQuizList();
                            QuizList = updatedQuizList;
                            QuizName = "";
                            QuizTime = 180;
                        },
                        (o) => !string.IsNullOrEmpty(QuizName)
                        && QuizTime is int
                        && QuizTime > 0
                    );
                return dodajQuiz;
            }
        }

        private ICommand usunQuiz;
        public ICommand UsunQuiz
        {
            get
            {
                if (usunQuiz == null)
                    usunQuiz = new RelayCommand(
                        (o) =>
                        {
                            if (SelectedQuiz != null)
                            {
                                dbConnection.DeleteQuiz(SelectedQuiz.Id);
                                ObservableCollection<Quiz> updatedQuizList = dbConnection.GetQuizList();
                                QuizList = updatedQuizList;
                            }
                        },
                        (o) => SelectedQuiz != null);
                return usunQuiz;
            }
        }

        private ICommand usunQuest;
        public ICommand UsunQuest
        {
            get
            {
                if (usunQuest == null)
                    usunQuest = new RelayCommand(
                        (o) =>
                        {
                            if (SelectedQuest != null)
                            {
                                dbConnection.DeleteQuest(SelectedQuest.Id_quest);
                                ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(SelectedQuiz.Id);
                                QuestList = updatedQuestList;
                            }
                        },
                        (o) => SelectedQuest != null);
                return usunQuest;
            }
        }

        private ICommand aktualizujQuiz;
        public ICommand AktualizujQuiz
        {
            get
            {
                if (aktualizujQuiz == null)
                    aktualizujQuiz = new RelayCommand(
                        (o) =>
                        {
                            dbConnection.AktualizujQuiz(SelectedQuiz.Id, QuizName, QuizTime);
                            ObservableCollection<Quiz> updatedQuizList = dbConnection.GetQuizList();
                            QuizList = updatedQuizList;
                        },
                        (o) => SelectedQuiz != null
                        && !string.IsNullOrEmpty(QuizName)
                        && QuizTime is int
                        && QuizTime > 0);
                return aktualizujQuiz;
            }
        }

        private ICommand aktualizujQuest;
        public ICommand AktualizujQuest
        {
            get
            {
                if (aktualizujQuest == null)
                    aktualizujQuest = new RelayCommand(
                        (o) =>
                        {
                            dbConnection.AktualizujQuest(SelectedQuest.Id_quest,
                                Szyfrowanko.Szyfrowanie(Pytanie, "modelmvvm"),
                                Szyfrowanko.Szyfrowanie(Odp1, "modelmvvm"),
                                Szyfrowanko.Szyfrowanie(Odp2, "modelmvvm"),
                                Szyfrowanko.Szyfrowanie(Odp3, "modelmvvm"),
                                Szyfrowanko.Szyfrowanie(Odp4, "modelmvvm"),
                                Szyfrowanko.SzyfrNumer(SelectedNumber));
                            ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(SelectedQuiz.Id);
                            QuestList = updatedQuestList;
                        },
                        (o) => SelectedQuest != null
                        && !string.IsNullOrEmpty(Pytanie)
                        && !string.IsNullOrEmpty(Odp1)
                        && !string.IsNullOrEmpty(Odp2)
                        && !string.IsNullOrEmpty(Odp3)
                        && !string.IsNullOrEmpty(Odp4)
                        && SelectedQuiz != null);
                return aktualizujQuest;
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
                OnPropertyChanged(nameof(Pytanie));
            }
        }

        private string odp1 = "";
        public string Odp1
        {
            get => odp1;
            set
            {
                odp1 = value;
                OnPropertyChanged(nameof(Odp1));
            }
        }

        private string odp2 = "";
        public string Odp2
        {
            get => odp2;
            set
            {
                odp2 = value;
                OnPropertyChanged(nameof(Odp2));
            }
        }

        private string odp3 = "";
        public string Odp3
        {
            get => odp3;
            set
            {
                odp3 = value;
                OnPropertyChanged(nameof(Odp3));
            }
        }

        private string odp4 = "";
        public string Odp4
        {
            get => odp4;
            set
            {
                odp4 = value;
                OnPropertyChanged(nameof(Odp4));
            }
        }

        private int quizTime = 180;
        public int QuizTime
        {
            get => quizTime;
            set
            {
                quizTime = value;
                OnPropertyChanged(nameof(QuizTime));
            }
        }

        private string quizName = "";
        public string QuizName
        {
            get => quizName;
            set
            {
                quizName = value;
                OnPropertyChanged($"{nameof(QuizName)}");
            }
        }

        private Quiz selectedQuiz;
        public Quiz SelectedQuiz
        {
            get => selectedQuiz;
            set
            {
                selectedQuiz = value;
                OnPropertyChanged(nameof(SelectedQuiz));
                if (selectedQuiz != null)
                {
                    ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(selectedQuiz.Id);
                    QuestList = updatedQuestList;
                    QuizName = SelectedQuiz.Name;
                    QuizTime = SelectedQuiz.Czas;
                }
                else
                {
                    ObservableCollection<Quest> updatedQuestList = dbConnection.GetQuestList(-1);
                    QuestList = updatedQuestList;
                    QuizName = "";
                    QuizTime = 180;
                }
                
            }
        }

        private Quest selectedQuest;
        public Quest SelectedQuest
        {
            get => selectedQuest;
            set
            {
                selectedQuest = value;
                OnPropertyChanged(nameof(SelectedQuest));
                if (selectedQuest != null)
                {
                    Pytanie = Szyfrowanko.Odszyfrowanie(SelectedQuest.Tresc, "modelmvvm");
                    Odp1 = Szyfrowanko.Odszyfrowanie(SelectedQuest.Odp_1, "modelmvvm");
                    Odp2 = Szyfrowanko.Odszyfrowanie(SelectedQuest.Odp_2, "modelmvvm");
                    Odp3 = Szyfrowanko.Odszyfrowanie(SelectedQuest.Odp_3, "modelmvvm");
                    Odp4 = Szyfrowanko.Odszyfrowanie(SelectedQuest.Odp_4, "modelmvvm");
                    SelectedNumber = Szyfrowanko.DeszyfrNumer(selectedQuest.Poprawna_odp);
                }
                else
                {
                    Pytanie = "";
                    Odp1 = "";
                    Odp2 = "";
                    Odp3 = "";
                    Odp4 = "";
                    SelectedNumber = 1;
                }
            }
        }

        private int selectedNumber;
        public int SelectedNumber
        {
            get { return selectedNumber; }
            set
            {
                selectedNumber = value;
                OnPropertyChanged(nameof(SelectedNumber));
            }
        }

        private ICommand exterminacja;
        public ICommand Exterminacja
        {
            get
            {
                if (exterminacja == null)
                    exterminacja = new RelayCommand(
                        (o) =>
                        {
                            dbConnection.Exterminatus();
                            ObservableCollection<Quiz> updatedQuizList = dbConnection.GetQuizList();
                            QuizList = updatedQuizList;
                        },
                        (o) => true);
                return exterminacja;
            }
        }
    }
}
