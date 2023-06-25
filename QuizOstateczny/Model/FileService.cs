using System.IO;

namespace QuizOstateczny.Model
{
    public class FileService
    {
        private static readonly string filePath = "C:\\Users\\barti\\Downloads\\QuizID.txt";

        public static void SaveToFile(int quizID)
        {
            File.WriteAllText(filePath, string.Empty);

            File.WriteAllText(filePath, quizID.ToString());
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
    }
}
