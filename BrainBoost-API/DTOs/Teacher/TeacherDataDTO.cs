namespace BrainBoost_API.DTOs.Teacher
{
    public class TeacherDataDTO
    {
        public int UserId { get; set; }
        public int YearsOfExperience { get; set; }
        public IFormFile? Photo { get; set; }
        public string AboutYou { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Career { get; set; }
    }
}
