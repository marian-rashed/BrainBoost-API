using AutoMapper;
using BrainBoost_API.DTOs.Course;
using BrainBoost_API.DTOs.Question;
using BrainBoost_API.DTOs.Quiz;
using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class QuizRepository : Repository<Quiz> , IQuizRepository
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper mapper;

        public QuizRepository(ApplicationDbContext context,IMapper mapper) : base(context)
        {
            this.Context = context;
            this.mapper = mapper;
        }
        public QuizDTO getCrsQuiz(Quiz quiz,IEnumerable<Question> question, bool IsTaken)
        {
            QuizDTO q=null;
            try
            {
                 q = mapper.Map<QuizDTO>(quiz);
                q.Question = mapper.Map<IEnumerable<QuestionDTO>>(question).ToList();
                q.QuizState = IsTaken;
            }
            catch (Exception ex) { 
            }
           
            return q;

        }
    }
}
