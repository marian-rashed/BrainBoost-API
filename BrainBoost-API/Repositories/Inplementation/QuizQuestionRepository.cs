using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Interfaces;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class QuizQuestionRepository: Repository<QuizQuesitons>, IQuizQuestionRepository
    {
        private readonly ApplicationDbContext Context;
        public QuizQuestionRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
