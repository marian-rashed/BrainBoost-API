using Microsoft.AspNetCore.Identity;

namespace BrainBoost_API.Models
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsDeleted { get; set; } 
    }
}
