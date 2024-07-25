using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class VideoState
    {
        public int Id { get; set; }
        [ForeignKey("Video")]
        public int VideoId { get; set; }
        [ForeignKey("StudentEnrolledCourses")]
        public int StudentEnrolledCourseId { get; set; }
        public Video Video { get; set; }
        public bool State { get; set; } = false;

        public StudentEnrolledCourses StudentEnrolledCourses { get; set; }
    }
}
