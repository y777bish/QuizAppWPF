using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOstateczny.Model
{
    internal class Answer
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; } = false;
        public bool IsChoosen { get; set; } = false;
    }
}
