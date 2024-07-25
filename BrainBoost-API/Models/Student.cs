using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int? NumOfCrsEnrolled { get; set; }
        public int? NumOfCertificates { get; set; }
        public int? NumOfCrsSaved { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? PictureUrl { get; set; }
        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; } 

        public ApplicationUser? AppUser { get; set; }

        public List<StudentEnrolledCourses>? EnrolledCourses { get; set; }
        public List<StudentSavedCourses>? SavedCourses { get; set; }
        public List<comment> comments { get; set; }
    }
}
