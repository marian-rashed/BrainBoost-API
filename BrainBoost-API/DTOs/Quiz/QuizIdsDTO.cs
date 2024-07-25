namespace BrainBoost_API.DTOs.Quiz
{
    public class QuizIdsDTO
    {
       public int CourseId { get; set; }
       public int QuizId { get; set; }
       public List<int>? QuestionId { get; set; }
       public List<int>? AnswerId { get; set; }
    }
}
