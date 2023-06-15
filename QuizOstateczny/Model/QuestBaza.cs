using SQLite;

namespace QuizOstateczny.Model
{
    public class QuestBaza
    {
        [PrimaryKey, AutoIncrement]
        public int id_quest { get; set; }
        public string tresc { get; set; }
        public string odp1 { get; set; }
        public string odp2 { get; set; }
        public string odp3 { get; set; }
        public string odp4 { get; set; }
        public string poprawna_odp { get; set; }
        public int quiz { get; set; }
    }
}