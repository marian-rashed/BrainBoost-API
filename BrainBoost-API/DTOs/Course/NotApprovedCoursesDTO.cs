namespace BrainBoost_API.DTOs.Course
{
    public class NotApprovedCoursesDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? photoUrl { get; set; }
        public int Price { get; set; }
        public int? Rate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; } = false;

    }
}
