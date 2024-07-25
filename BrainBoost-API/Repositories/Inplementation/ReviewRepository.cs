using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class ReviewRepository : Repository<Review> , IReviewRepository
    {
        private readonly ApplicationDbContext Context;
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;

        }
    }
}
