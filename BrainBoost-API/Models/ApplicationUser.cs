using Microsoft.AspNetCore.Identity;

namespace BrainBoost_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; } 
        public string? Fname { get; set; }
        public string? Lname { get; set; }

    }
}
