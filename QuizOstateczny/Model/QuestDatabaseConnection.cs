using QuizOstateczny.Model;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Media.Animation;

public class QuestDatabaseConnection
{
    private SQLiteConnection connection;

    public QuestDatabaseConnection(string databasePath)
    {
        connection = new SQLiteConnection(databasePath);
    }

    public void AddQuest(QuestBaza quest)
    {
        string query = "INSERT INTO quests (tresc, odp1, odp2, odp3, odp4, poprawna_odp, quiz) VALUES (?, ?, ?, ?, ?, ?, ?)";
        connection.Execute(query, quest.tresc, quest.odp1, quest.odp2, quest.odp3, quest.odp4, quest.poprawna_odp, quest.quiz);
    }

    public void AddQuiz(QuizBaza quiz)
    {
        string query = "INSERT INTO quizy (nazwa, czas) VALUES (?, ?)";
        connection.Execute(query, quiz.nazwa, quiz.czas);
    }

    public void DeleteQuiz(int quizId)
    {
        connection.Execute("DELETE FROM quizy WHERE id_test = ?", quizId);

        connection.Execute("DELETE FROM quests WHERE quiz = ?", quizId);
    }

    public void DeleteQuest(int questId)
    {
        connection.Execute("DELETE FROM quests WHERE id_quest = ?", questId);
    }

    public ObservableCollection<Quiz> GetQuizList()
    {
        string query = "SELECT * FROM quizy";
        List<QuizBaza> quizList = connection.Query<QuizBaza>(query);
        ObservableCollection<Quiz> quizList2 = new ObservableCollection<Quiz>();
        foreach (QuizBaza quizBaza in quizList)
        {
            Quiz quiz = new Quiz();
            quiz.Id = quizBaza.id_test;
            quiz.Name = quizBaza.nazwa;
            quiz.Czas = quizBaza.czas;

            quizList2.Add(quiz);
        }
        return quizList2;
    }

    public ObservableCollection<Quest> GetQuestList(int idQuiz)
    {
        string query = "SELECT * FROM quests WHERE quiz = ?";
        List<QuestBaza> questBazaList = connection.Query<QuestBaza>(query, idQuiz);
        ObservableCollection<Quest> questList = new ObservableCollection<Quest>();
        foreach (QuestBaza questBaza in questBazaList)
        {
            Quest quest = new Quest();
            quest.Id_quest = questBaza.id_quest;
            quest.Tresc = questBaza.tresc;
            quest.Odp_1 = questBaza.odp1;
            quest.Odp_2 = questBaza.odp2;
            quest.Odp_3 = questBaza.odp3;
            quest.Odp_4 = questBaza.odp4;
            quest.Poprawna_odp = questBaza.poprawna_odp;
            quest.Quiz = questBaza.quiz;

            questList.Add(quest);
        }
        return questList;
    }

    public void AktualizujQuiz(int  idQuiz, string newName, int newTime)
    {
        string query = $"UPDATE quizy SET nazwa = '{newName}', czas = {newTime} WHERE id_test = {idQuiz}";
        connection.Execute(query);
    }

    public void AktualizujQuest(int idQuest, string newTresc, string newOdp1, string newOdp2, string newOdp3, string newOdp4, string poprawnaOdp)
    {
        string query = $"UPDATE quests SET tresc = '{newTresc}', odp1 = '{newOdp1}', odp2 = '{newOdp2}', odp3 = '{newOdp3}', odp4 = '{newOdp4}', poprawna_odp = '{poprawnaOdp}' WHERE id_quest = {idQuest}";
        connection.Execute(query);
    }

    public void Exterminatus()
    {
        connection.Execute("DELETE FROM quests");
        connection.Execute("DELETE FROM quizy");
    }
}