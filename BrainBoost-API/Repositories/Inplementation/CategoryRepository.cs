using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext Context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
        public void save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
