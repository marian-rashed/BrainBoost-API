using BrainBoost_API.DTOs.Teacher;

namespace BrainBoost_API.DTOs.Course
{
    public class CourseEarningsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? photoUrl { get; set; }
        public int Price { get; set; }
        public int? Rate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public CourseCardTeacherDataDto Teacher { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal TotalInstructorEarnings { get; set; }
        public decimal TotalWebsiteEarnings { get; set; }
    }
}
