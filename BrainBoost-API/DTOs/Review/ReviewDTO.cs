namespace BrainBoost_API.DTOs.Review
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int Rate { get; set; }
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
