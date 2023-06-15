namespace QuizOstateczny.Model
{
    public class Quest
    {
        public int Id_quest { get; set; }

        public string Tresc { get; set; }

        public string Odp_1 { get; set; }

        public string Odp_2 { get; set; }

        public string Odp_3 { get; set; }

        public string Odp_4 { get; set; }

        public string Poprawna_odp { get; set; }

        public int Quiz { get; set; }

    }
}
