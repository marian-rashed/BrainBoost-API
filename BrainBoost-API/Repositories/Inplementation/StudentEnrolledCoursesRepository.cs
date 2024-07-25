using BrainBoost_API.Models;
using System.Linq;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class StudentEnrolledCoursesRepository : Repository<StudentEnrolledCourses>, IStudentEnrolledCoursesRepository
    {
        private readonly ApplicationDbContext Context;
        public StudentEnrolledCoursesRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
        public int GetNumOfStdsOfCourseById(int courseId)
        {
            int numOfStudents = Context.StudentEnrolledCourses
                .Where(sc => !sc.IsDeleted && sc.CourseId == courseId)
                .Count();

            return numOfStudents;
        }
    }
}
