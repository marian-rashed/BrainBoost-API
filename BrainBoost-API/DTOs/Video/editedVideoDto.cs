namespace BrainBoost_API.DTOs.video
{
    public class editedVideoDto
    {
        public int id {  get; set; }
        public string title { get; set; }
        public IFormFile videoFile { get; set; }
        public int crsId { get; set; }
    }
}
