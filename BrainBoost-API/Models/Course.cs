using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? LongDescription { get; set; }
        public string? photoUrl { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? Language { get; set; }
        public double? Durtion { get; set; }
        public string? Level { get; set; }
        public int Price { get; set; }
        public int? Rate { get; set; }
        public bool IsDeleted { get; set; } 
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public bool IsApproved { get; set; } = false;
        public string? CertificateHeadline { get; set; }
        public string? CertificateAppreciationParagraph { get; set; }
        public Teacher? Teacher { get; set; }
        public Category? Category { get; set; }
        public List<Video>? videos { get; set; }
        public Quiz? quiz { get; set; }
        public List<WhatToLearn>? WhatToLearn { get; set; }
        public List<StudentEnrolledCourses>? EnrolledCourses { get; set; }
        public List<StudentSavedCourses>? SavedCourses { get; set; }
    }
}
