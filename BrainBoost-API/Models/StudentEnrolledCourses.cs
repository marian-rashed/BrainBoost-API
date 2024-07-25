using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrainBoost_API.Models
{
    public class StudentEnrolledCourses
    {
        [Key]
        public int Id { get; set; }

        public bool QuizState { get; set; }
        public bool CertificateState { get; set; }
        public bool hasFinishedallVideos { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public bool IsDeleted { get; set; } = false; 
        public Student? Student { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public List<VideoState> videoStates { get; set; }

    }
}
