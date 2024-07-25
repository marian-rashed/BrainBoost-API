using BrainBoost_API.DTOs.Review;
using BrainBoost_API.DTOs.Teacher;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.DTOs.Course
{
    public class CourseDetailsDto
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
        public bool IsApproved { get; set; } = false;
        public CourseDetailsTeacherDataDto TeacherDataDto { get; set; } =new CourseDetailsTeacherDataDto();
        public List<ReviewDTO>? Review { get; set; }
        public List<WhatToLearnDTO>? WhatToLearn { get; set; }


    }
}
