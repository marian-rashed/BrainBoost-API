using AutoMapper;
using BrainBoost_API.DTOs.Course;
using BrainBoost_API.DTOs.Review;
using BrainBoost_API.DTOs.Teacher;
using BrainBoost_API.DTOs.Video;
using BrainBoost_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper mapper;

        public CourseRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.Context = context;
            this.mapper = mapper;
        }

        public CourseDetailsDto getCrsDetails(Course crs, List<Review> review)
        {
            if (crs != null)
            {

                CourseDetailsDto crsDetails = mapper.Map<CourseDetailsDto>(crs);
                crsDetails.NumOfVideos = crs.videos?.Count;
                crsDetails.Review = mapper.Map<IEnumerable<ReviewDTO>>(review).ToList();
                var reviewDict = review.ToDictionary(vs => vs.Id);
                foreach (var rev  in crsDetails.Review)
                {
                    if (reviewDict.TryGetValue(rev.Id, out var revv))
                    {
                        rev.PhotoUrl = revv.Student.PictureUrl;
                        rev.Name = revv.Student.Fname + " " + revv.Student.Lname;
                    }
                }
                crsDetails.WhatToLearn = mapper.Map<IEnumerable<WhatToLearnDTO>>(crs.WhatToLearn).ToList();
                crsDetails.TeacherDataDto = mapper.Map<CourseDetailsTeacherDataDto>(crs.Teacher);



                return crsDetails;
            }
            return new CourseDetailsDto();
        }
        public dynamic GetFilteredCourses(CourseFilterationDto filter, string? includeProps = null)
        {
            IQueryable<Course> courses = GetAll(includeProps).AsQueryable();

            if (filter.CategoryName != null && filter.CategoryName != "all")
            {
                courses = courses.Where(c => c.Category.Name == filter.CategoryName);
            }

            if (filter.Price != -1)
            {
                if (filter.Price == 0)
                {
                    courses = courses.Where(c => c.Price == filter.Price);
                }
                else if (filter.Price > 0)
                {
                    courses = courses.Where(c => c.Price >= filter.Price);
                }
            }

            if (filter.Rate != -1)
            {
                courses = courses.Where(c => c.Rate == filter.Rate);
            }

            if (filter.Durtion > -1)
            {
                if (filter.Durtion == 0)
                {
                    courses = courses.Where(c => c.Durtion >= 0 && c.Durtion <= 5);
                }
                else if (filter.Durtion == 6)
                {
                    courses = courses.Where(c => c.Durtion >= 6 && c.Durtion <= 10);
                }
                else if (filter.Durtion == 11)
                {
                    courses = courses.Where(c => c.Durtion >= 11 && c.Durtion <= 15);
                }
                else if (filter.Durtion == 15)
                {
                    courses = courses.Where(c => c.Durtion >= 15);
                }
            }
            var Count = courses.Count();
            // Apply pagination after all filters
            List<Course> filteredCourses = courses.Skip((filter.PageNumber - 1) * filter.PageSize)
                                         .Take(filter.PageSize)
                                         .ToList();

            return new { filteredCourses = filteredCourses , Count = Count };
        }
        public List<Course> SearchCourses(string searchString, string? includeProps)
        {
            
             var courses = GetList(c => c.Name.Contains(searchString) || c.Description.Contains(searchString)
                            || c.Teacher.Fname.Contains(searchString) || c.Teacher.Lname.Contains(searchString), includeProps);


            if (courses != null)
            {
                return courses.ToList();
            }
            else
            {
                return new List<Course>();
            }
        }
        public CertificateDTO getCrsCertificate(Course crs, string s,string teacherName,double? duration)
        {
            if (crs != null)
            {
                var cert = mapper.Map<CertificateDTO>(crs);
                cert.StdName = s;
                cert.TeacherName = teacherName;
                cert.Duration = duration;
                return cert;

            }
            return new CertificateDTO();

        }

        public CourseTakingDTO GetCourseTaking(Course takingcourse, IEnumerable<Course> relatedCourses, StudentEnrolledCourses states)
        {
            var Crs= mapper.Map<CourseTakingDTO>(takingcourse);
            Crs.CourseCardData=mapper.Map<IEnumerable<CourseCardDataDto>>(relatedCourses).ToList();
            Crs.states=mapper.Map<StateDTO>(states);
            Crs.WhatToLearn = mapper.Map<IEnumerable<WhatToLearnDTO>>(takingcourse.WhatToLearn).ToList();
            Crs.TeacherDataDto = mapper.Map<CourseDetailsTeacherDataDto>(takingcourse.Teacher);


            return Crs;
        }
        public StateDTO getCrsStates(StudentEnrolledCourses states)
        {
            var States = mapper.Map<StateDTO>(states);
            return States;
        } 
        public IEnumerable<Course> GetNotApprovedCourses(string? includeProps = null)
        {
            //IQueryable<Course> courses = GetAll(includeProps).AsQueryable();
            IQueryable<Course> courses = Context.Courses.IgnoreQueryFilters();

            courses = courses.IgnoreQueryFilters().Where(c => c.IsApproved == false&& c.IsDeleted==false);
            var filteredCourses = new List<Course>();
            filteredCourses = courses.ToList();
            return filteredCourses;
        }
        public Course GetNotApprovedCoursesbyid(int id)
        {
            
            Course course = Context.Courses.IgnoreQueryFilters().Where(c => c.Id == id ).FirstOrDefault();
            return course;
        }
        public int GetTotalNumOfCourse()
        {
            int numofCourse = Context.Courses.Count<Course>();
            return numofCourse;
        }

        public List<Course> GetLastThreeCourses()
        {
            List<Course> lastThreeCourses = Context.Courses
                                            .OrderByDescending(c => c.LastUpdate)
                                            .Take(3)
                                            .ToList();
            return lastThreeCourses;
        }
        public List<CourseEarningsDto> GetTop3CoursesByEarnings()
        {
            var topCourses = Context.Earnings
                .Where(e => e.enrollment != null && e.enrollment.Course != null)
                .GroupBy(e => e.enrollment.Course)
                .OrderByDescending(g => g.Sum(e => e.Amount))
                .Take(3)
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
        public IEnumerable<CourseCardDataDto> GetTop4RatedCrs()
        {
            var top4Courses = Context.Courses
                                .OrderByDescending(c => c.LastUpdate) // Ensure you order them
                                .Take(4)
                                .ToList()
                                .Select(c =>
                                {
                                    var dto = mapper.Map<CourseCardDataDto>(c);
                                    return dto;
                                }).ToList();
            return top4Courses;
        }
        public IEnumerable<CourseCardDataDto> GetCoursesByStdId(int stdId)
        {
            var courses = from SEC in Context.StudentEnrolledCourses
                          join C in Context.Courses on SEC.CourseId equals C.Id
                          where SEC.StudentId == stdId
                          select mapper.Map<CourseCardDataDto>(C);
            return courses;
        }
        
    }
}
