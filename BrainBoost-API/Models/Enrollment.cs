using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        // Additional properties
        public string? SubscribtionsStatus { get; set; }
        public string? TransactionNo { get; set; }
        public string? CheckUrl { get; set; }
        public string? orderNumber { get; set; }


        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
