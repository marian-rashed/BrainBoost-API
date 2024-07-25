using BrainBoost_API.DTOs.Course;
using BrainBoost_API.DTOs.Teacher;
using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public interface IEarningsRepository : IRepository<Earnings>
    {
        decimal GetTotalInstructorEarnings();
        decimal GetTotalWebsiteEarnings();
        decimal GetTotalEarning();
        List<CourseEarningsDto> GetCoursesAndEarningsForInstructor(int instructorId);
        List<TeacherEarningData> GetTeachersAndEarnings();
    }
}
