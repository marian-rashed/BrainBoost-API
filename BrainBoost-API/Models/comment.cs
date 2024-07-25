using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class comment
    {
        public int Id { get; set; }
        [ForeignKey("video")]
        public int VideoId { get; set; }
        [ForeignKey("student")]
        public int studentId { get; set; }
        public string? Content { get; set; }
        public string? StudentPhoto { get; set; }
        public string? StudentName { get; set; }

        public DateTime CommentDate { get; set; }
        public Student? student { get; set; }
        public Video? video { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
