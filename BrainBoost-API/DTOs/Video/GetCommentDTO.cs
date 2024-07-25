namespace BrainBoost_API.DTOs.video
{
    public class GetCommentDTO
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; }
        public int studentId { get; set; }
        public string StudentPhoto { get; set; }
        public string StudentName { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
