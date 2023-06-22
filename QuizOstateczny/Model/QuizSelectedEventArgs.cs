using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOstateczny.Model
{
    public class QuizSelectedEventArgs : EventArgs
    {
        public int WybranyQuiz { get; }

        public QuizSelectedEventArgs(int wybranyQuiz)
        {
            WybranyQuiz = wybranyQuiz;
        }
    }
}
