using System.IO;

namespace QuizOstateczny.Model
{
    public class FileService
    {
        //private static readonly string filePath = "C:\\Users\\lukas\\OneDrive\\Desktop\\QuizID.txt";
        private static readonly string filePath = "C:\\Users\\barti\\Downloads\\QuizID.txt";

        //private static readonly string filePath2 = "C:\\Users\\lukas\\OneDrive\\Desktop\\QuizPts.txt";
        private static readonly string filePath2 = "C:\\Users\\barti\\Downloads\\QuizPts.txt";

        public static void SaveToFile(int quizID)
        {
            File.WriteAllText(filePath, string.Empty);

            File.WriteAllText(filePath, quizID.ToString());
        }

        public static void SavePtsToFile(int quizID)
        {
            File.WriteAllText(filePath2, string.Empty);

            File.WriteAllText(filePath2, quizID.ToString());
        }

        public static int ReadQuizIDFromFile()
        {
            int quizID = 0;

            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                int.TryParse(content, out quizID );
            }
            return quizID;
        }

        public static int ReadPtsFromFile()
        {
            int pts = 0;

            if (File.Exists(filePath2))
            {
                string content = File.ReadAllText(filePath2);
                int.TryParse(content, out pts);
            }
            return pts;
        }
    }
}
