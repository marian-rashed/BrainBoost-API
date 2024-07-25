using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class AnswerRepository : Repository<Answer> , IAnswerRepository
    {
        private readonly ApplicationDbContext Context;
        public AnswerRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
