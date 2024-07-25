using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.DTOs.video
{
    public class addCommentDTO
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; }
    }
}
