using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class FacebookUserRepository : Repository<FacebookUser> ,IFacebookUserRepository
    {
        private readonly ApplicationDbContext Context;
        public FacebookUserRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
