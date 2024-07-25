namespace BrainBoost_API.DTOs.Video
{
    public class VideoDTO
    {
        public string Title { get; set; }
        public IFormFile VideoFile { get; set; }
        public int Chapter { get; set; }

    }
}
