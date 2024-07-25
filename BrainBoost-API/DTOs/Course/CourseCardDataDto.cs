using BrainBoost_API.DTOs.Teacher;
using BrainBoost_API.Models;

namespace BrainBoost_API.DTOs.Course
{
    public class CourseCardDataDto
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? photoUrl { get; set; }
        public int Price { get; set; }
        public int? Rate { get; set; }
        public int Durtion {  get; set; }
        public string Description {  get; set; }
        public CourseCardTeacherDataDto Teacher { get; set; }
    }
}
