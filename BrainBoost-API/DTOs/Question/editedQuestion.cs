using BrainBoost_API.DTOs.Answer;

namespace BrainBoost_API.DTOs.Question
{
    public class editedQuestion
    {
        public int Id { get; set; }
        public string HeadLine { get; set; }
        public int Degree { get; set; }
        public List<editedChoice> Choices { get; set; }

    }
}
