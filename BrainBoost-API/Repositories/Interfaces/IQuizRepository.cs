using BrainBoost_API.DTOs.Quiz;
using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        public QuizDTO getCrsQuiz(Quiz quiz, IEnumerable<Question> question,bool IsTaken);
       
    }
}
