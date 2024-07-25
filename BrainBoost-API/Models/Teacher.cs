using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int? NumOfCrs { get; set; }
        public int? NumOfFollowers { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? PictureUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? AboutYou { get; set; }
        public string? Career { get; set; }
        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ApplicationUser ?AppUser { get; set; }
        public List<Course>? Crs { get; set; }
    }
}
