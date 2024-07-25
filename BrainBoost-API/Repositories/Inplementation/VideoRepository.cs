using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class VideoRepository : Repository<Video> , IVideoRepository
    {
        private readonly ApplicationDbContext Context;
        public VideoRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
