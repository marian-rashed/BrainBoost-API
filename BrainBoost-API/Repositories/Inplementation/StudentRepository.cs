using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Interfaces;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class StudentRepository : Repository<Student> , IStudentRepository
    {
        private readonly ApplicationDbContext Context;
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
        public int GetTotalNumOfStudent()
        {
            int numofstudent = Context.Students.Count<Student>();
            return numofstudent;
        }
        public List<Student> GetTopStudents()
        {
            List<Student> students = Context.Students.
                OrderByDescending(s => s.NumOfCertificates)
                .Take(6).ToList();
            return students;
        }
        public int GetTotalNumOfEnrolledCourses()
        {
            int numofenrolledcourses=Context.StudentEnrolledCourses
                .Where(sec=>!sec.IsDeleted)
                .Select(sec=>sec.CourseId)
                .Distinct()
                .Count();
            return numofenrolledcourses;
        }
    }
}
