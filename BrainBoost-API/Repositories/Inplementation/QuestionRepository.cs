using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class QuestionRepository : Repository<Question> , IQuestionRepository
    {
        private readonly ApplicationDbContext Context;
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
