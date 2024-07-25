using BrainBoost_API.DTOs.Question;
using BrainBoost_API.Models;

namespace BrainBoost_API.DTOs.Quiz
{
    public class insertedQuiz
    {
        public int NumOfQuestions { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public List<insertedQuestion> Questions { get; set; }
    }
}
