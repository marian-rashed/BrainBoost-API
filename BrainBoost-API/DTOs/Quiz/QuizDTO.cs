using BrainBoost_API.DTOs.Question;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.DTOs.Quiz
{
    public class QuizDTO
    {
        public int NumOfQuestions { get; set; }
        public int Degree { get; set; }
        public bool QuizState { get; set; }
        public int MinDegree { get; set; }
        public bool IsDeleted { get; set; }      
        public List<QuestionDTO> Question { get; set; }   
    }
}
