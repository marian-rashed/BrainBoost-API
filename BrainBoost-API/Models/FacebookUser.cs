namespace BrainBoost_API.Models
{
    public class FacebookUser
    {
        public int Id { get; set; }
        public string? FacebookUserId { get; set; }
        public string? OriginalApplicationUserId { get; set; }

        public bool IsDeleted { get; set; } 
        //public ApplicationUser ApplicationUser { get; set;}
    }
}
