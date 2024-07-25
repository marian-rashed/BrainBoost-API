using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class QuizQuesitons
    {
        public int Id { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public bool IsDeleted { get; set; } 
        public Question? Question { get; set; }
        public Quiz? Quiz { get; set; }
    }
}
