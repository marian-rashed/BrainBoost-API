namespace BrainBoost_API.DTOs.Admin
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? PictureUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
