using BrainBoost_API.DTOs.Teacher;

namespace BrainBoost_API.DTOs.Course
{
    public class CourseTakingDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string? photoUrl { get; set; }
        public string? LongDescription { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? Language { get; set; }
        public double? Durtion { get; set; }
        public string? Level { get; set; }
        public int? Rate { get; set; }
        public int? NumOfRates { get; set; }
        public int? NumOfVideos { get; set; }
        public StateDTO? states { get; set; }
        public List<WhatToLearnDTO>? WhatToLearn { get; set; }
        public CourseDetailsTeacherDataDto TeacherDataDto { get; set; }
        public List<CourseCardDataDto>? CourseCardData { get; set; }

    }
}
