using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;

namespace BrainBoost_API.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        int GetTotalNumOfStudent();
        List<Student> GetTopStudents();
        int GetTotalNumOfEnrolledCourses();
    }
}
