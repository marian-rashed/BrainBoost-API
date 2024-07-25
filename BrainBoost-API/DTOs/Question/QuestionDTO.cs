using BrainBoost_API.DTOs.Answer;

namespace BrainBoost_API.DTOs.Question
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public int Degree { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
