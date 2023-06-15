using SQLite;

namespace QuizOstateczny.Model
{
    public class QuizBaza
    {
        [PrimaryKey, AutoIncrement]
        public int id_test { get; set; }

        public string nazwa { get; set; }

        public int czas { get; set; }
    }
}
