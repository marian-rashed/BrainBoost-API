using BrainBoost_API.DTOs.Question;

namespace BrainBoost_API.DTOs.Quiz
{
    public class editedQuizz
    {
        public int Id { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int NumOfQuestions { get; set; }
        public List<editedQuestion> Questions { get; set; }
    }
}
