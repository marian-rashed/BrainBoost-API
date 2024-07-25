namespace BrainBoost_API.DTOs.video
{
    public class VideoStateDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? VideoUrl { get; set; }
        public int Chapter { get; set; }
        public bool State { get; set; } = false;
    }
}
