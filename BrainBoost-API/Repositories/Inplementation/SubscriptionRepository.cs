using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class SubscriptionRepository : Repository<subscription> , ISubscriptionRepository
    {
        private readonly ApplicationDbContext Context;
        public SubscriptionRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
