using BrainBoost_API.DTOs.Course;
using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public interface ICourseRepository : IRepository<Course>
    {
        CourseDetailsDto getCrsDetails(Course crs, List<Review> review);
        CertificateDTO getCrsCertificate(Course crs, string s, string teacherName, double ?duration);
        dynamic GetFilteredCourses(CourseFilterationDto filter, string? includeProps);
        List<Course> SearchCourses(string searchString, string? includeProps);

        CourseTakingDTO GetCourseTaking(Course takingcourse,IEnumerable<Course> relatedCourses,StudentEnrolledCourses states );
        StateDTO getCrsStates(StudentEnrolledCourses states);

        IEnumerable<Course> GetNotApprovedCourses(string? includeProps = null);
        Course GetNotApprovedCoursesbyid(int id);
        int GetTotalNumOfCourse();
        List<Course> GetLastThreeCourses();
        List<CourseEarningsDto> GetTop3CoursesByEarnings();
        IEnumerable<CourseCardDataDto> GetTop4RatedCrs();
        IEnumerable<CourseCardDataDto> GetCoursesByStdId(int stdId);
    }
}
