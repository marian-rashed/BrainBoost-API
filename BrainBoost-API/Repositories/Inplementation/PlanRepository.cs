using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class PlanRepository : Repository<Plan> , IPlanRepository
    {
        private readonly ApplicationDbContext Context;
        public PlanRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
