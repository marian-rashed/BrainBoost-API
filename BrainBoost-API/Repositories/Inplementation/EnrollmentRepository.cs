using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class EnrollmentRepository : Repository<Enrollment> , IEnrollmentRepository
    {
        private readonly ApplicationDbContext Context;
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
