namespace QuizOstateczny.Model
{
    class Quest
    {
        public int Id_quest { get; set; }

        public string Tresc { get; set; }

        public string Odp_1 { get; set; }

        public string Odp_2 { get; set; }

        public string Odp_3 { get; set; }

        public string Odp_4 { get; set; }

        public int Poprawna_odp { get; set; }

        public int Nr_zadania { get; set; }

        public string Quiz { get; set; }

    }
}
