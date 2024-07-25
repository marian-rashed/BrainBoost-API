using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Video
    {
        public int Id { get; set; }
        public int Chapter { get; set; }
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Title { get; set; }
        public string? VideoUrl { get; set; }
        public Course? Course { get; set; }
        public List<comment> comments { get; set; }
    }
}
