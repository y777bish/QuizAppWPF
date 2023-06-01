using QuizOstateczny.Model;
using SQLite;

public class QuestDatabaseConnection
{
    private SQLiteConnection connection;

    public QuestDatabaseConnection(string databasePath)
    {
        connection = new SQLiteConnection(databasePath);
        connection.CreateTable<Quest>();
    }

    public void AddQuest(Quest quest)
    {
        connection.Insert(quest);
    }
}