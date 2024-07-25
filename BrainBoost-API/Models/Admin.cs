using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? PictureUrl { get; set; }
        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; }

        public ApplicationUser? AppUser { get; set; }
    }
}
