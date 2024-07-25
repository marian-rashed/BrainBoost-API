using BrainBoost_API.DTOs.Answer;
using BrainBoost_API.Enums;

namespace BrainBoost_API.DTOs.Question
{
    public class insertedQuestion
    {
        public string HeadLine { get; set; }
        public int Degree { get; set; }
        public List<insertedChoice> Choices { get; set; }
    }
}
