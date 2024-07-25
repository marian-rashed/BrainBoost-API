namespace BrainBoost_API.DTOs.Review
{
    public class ReviewWithCourseDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int Rate { get; set; }
        public int CourseId { get; set; }
    }
}
