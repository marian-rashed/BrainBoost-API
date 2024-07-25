using AutoMapper;
using BrainBoost_API.DTOs.Course;
using BrainBoost_API.DTOs.Teacher;
using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class EarningsRepository : Repository<Earnings>, IEarningsRepository
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper mapper;
        public EarningsRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.Context = context;
            this.mapper = mapper;
        }
        public decimal GetTotalInstructorEarnings()
        {
            var totalInstructorEarnings = Context.Earnings
                .Where(e => e.enrollment != null && e.enrollment.Course != null)
                .Sum(e => e.InstructorEarnings);

            return totalInstructorEarnings;
        }
        public decimal GetTotalWebsiteEarnings()
        {
            var totalWebsiteEarnings = Context.Earnings
                .Where(e => e.enrollment != null && e.enrollment.Course != null)
                .Sum(e => e.WebsiteEarnings);

            return totalWebsiteEarnings;
        }

        public decimal GetTotalEarning()
        {
            decimal TotalEarning = Context.Earnings
                .Where(e => e.enrollment != null && e.enrollment.Course != null)
                .Sum(e => e.WebsiteEarnings + e.InstructorEarnings);
            return TotalEarning;
        }

        public List<CourseEarningsDto> GetCoursesAndEarningsForInstructor(int instructorId)
        {
            var topCourses = Context.Earnings
                .Where(e => e.enrollment != null && e.enrollment.Course != null && e.enrollment.Course.TeacherId == instructorId)
                .GroupBy(e => e.enrollment.Course)
                .Select(g => new
                {
                    Course = g.Key,
                    TotalEarnings = g.Sum(e => e.Amount),
                    TotalInstructorEarnings = g.Sum(e => e.InstructorEarnings),
                    TotalWebsiteEarnings = g.Sum(e => e.WebsiteEarnings)
                })
                .ToList()
            .Select(x =>
            {
                var dto = mapper.Map<CourseEarningsDto>(x.Course);
                dto.TotalEarnings = x.TotalEarnings;
                dto.TotalInstructorEarnings = x.TotalInstructorEarnings;
                dto.TotalWebsiteEarnings = x.TotalWebsiteEarnings;
                return dto;
            })
                .ToList();

            return topCourses;
        }

        public List<TeacherEarningData> GetTeachersAndEarnings()
        {
            var teachers = Context.Earnings
                .Where(e => e.enrollment != null && e.enrollment.Course != null && e.enrollment.Course.Teacher != null)
                .GroupBy(e => e.enrollment.Course.Teacher)
                .Select(g => new
                {
                    Teacher = g.Key,
                    TotalEarnings = g.Sum(e => e.Amount),
                    TotalInstructorEarnings = g.Sum(e => e.InstructorEarnings),
                    TotalWebsiteEarnings = g.Sum(e => e.WebsiteEarnings)
                })
                .ToList()
            .Select(x =>
            {
                var dto = mapper.Map<TeacherEarningData>(x.Teacher);
                dto.TotalEarnings = x.TotalEarnings;
                dto.TotalInstructorEarnings = x.TotalInstructorEarnings;
                dto.TotalWebsiteEarnings = x.TotalWebsiteEarnings;
                return dto;
            })
                .ToList();

            return teachers;
        }




    }
}
