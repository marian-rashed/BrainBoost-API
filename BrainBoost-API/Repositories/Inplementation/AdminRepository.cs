using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Interfaces;
namespace BrainBoost_API.Repositories.Inplementation
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly ApplicationDbContext Context;
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }

    }
}
