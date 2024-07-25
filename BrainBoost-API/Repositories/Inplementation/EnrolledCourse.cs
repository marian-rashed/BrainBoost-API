using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Interfaces;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class EnrolledCourse : Repository<StudentEnrolledCourses>, IEnrolledCourses
    {
        private readonly ApplicationDbContext context;

        public EnrolledCourse(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
