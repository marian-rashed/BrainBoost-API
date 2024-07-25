namespace BrainBoost_API.DTOs.Course
{
    public class CourseFilterationDto
    {
        public string? CategoryName { get; set; }
        public int? Price { get; set; }
        public int? Rate { get; set; }
        public int? Durtion { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
