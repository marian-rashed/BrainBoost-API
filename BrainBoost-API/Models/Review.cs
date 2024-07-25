using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int Rate { get; set; }
        public bool IsDeleted { get; set; } 
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Student? Student { get; set; }
        public Course? Course { get; set; }


    }
}
