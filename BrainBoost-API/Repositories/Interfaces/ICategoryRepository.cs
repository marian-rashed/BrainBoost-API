using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void save();
    }
}
