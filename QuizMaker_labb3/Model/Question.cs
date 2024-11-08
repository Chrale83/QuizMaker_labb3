using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizMaker_labb3.Model
{
    public class Question
    {
        private string _query;
        public Question(string query, string correctAnswer,
            string inCorrectAnswer1, string inCorrectAnswer2, string inCorrectAnswer3)
        {
            Query = query;
            CorrectAnswer = correctAnswer;
            InCorrectAnswers = new string[3] { inCorrectAnswer1, inCorrectAnswer2, inCorrectAnswer3 };
        }
        [JsonConstructor]
        public Question()
        {
            Query = string.Empty;
            CorrectAnswer = string.Empty;
            InCorrectAnswers = [];
        }
        //public Question(string query, string correctAnswer, string[] incorrectAnswers)
        //{
        //    Query = query;
        //    CorrectAnswer = correctAnswer;
        //    InCorrectAnswers = incorrectAnswers;
        //}
        public string Query { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] InCorrectAnswers { get; set; }


    }

    
}
