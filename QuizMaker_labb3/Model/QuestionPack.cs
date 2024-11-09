using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker_labb3.Model
{
    public class QuestionPack
    {
        public QuestionPack(string name = "DefaultPackName", Difficulty difficulty = Difficulty.Medium, int timeLimitInSeconds = 60) //Default värden på de 2 sista argumenten
        {
            Name = name;
            Difficulty = difficulty;
            TimeLimitInSeconds = timeLimitInSeconds;
            Questions = new List<Question>();
        }

        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int TimeLimitInSeconds { get; set; }
        public List<Question> Questions { get; set; }

    }
    public enum Difficulty
    {
        Easy, Medium, Hard
    }
}

