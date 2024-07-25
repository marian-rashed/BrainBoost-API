using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int NumOfQuestions { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public List<QuizQuesitons>? Questions { get; set; }
    }
}
