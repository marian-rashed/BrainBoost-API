using AutoMapper;
using BrainBoost_API.DTOs.Quiz;
using BrainBoost_API.DTOs.Review;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper mapper;

        public ReviewController(IUnitOfWork UnitOfWork,IMapper mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("SetReview")]
        public async Task<IActionResult> SetReview(ReviewWithCourseDTO rev)

        {
            if (ModelState.IsValid)
            {

                string UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Student std = UnitOfWork.StudentRepository.Get(c => c.UserId == UserID);
                Review review = UnitOfWork.ReviewRepository.Get(c => c.StudentId == std.Id && c.CourseId == rev.CourseId);
                if (review == null)
                {
                    review = new Review()
                    {
                        StudentId = std.Id,
                        CourseId = rev.CourseId,
                        Content = rev.Content,
                    };
                    UnitOfWork.ReviewRepository.add(review);
                }
                else
                {
                    review.Content = rev.Content;
                    UnitOfWork.ReviewRepository.update(review);
                }
                UnitOfWork.save();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPost("SetRate")]
        public async Task<IActionResult> SetRate(ReviewWithCourseDTO rev)

        {
            if (ModelState.IsValid)
            {

                string UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Student std = UnitOfWork.StudentRepository.Get(c => c.UserId == UserID);
                Review review = UnitOfWork.ReviewRepository.Get(c => c.StudentId == std.Id && c.CourseId == rev.CourseId);
                var course = UnitOfWork.CourseRepository.Get(c => c.Id == rev.CourseId);
                if (review == null)
                {
                    review = new Review()
                    {
                        StudentId = std.Id,
                        CourseId = rev.CourseId,
                        Rate = rev.Rate,
                    };

                    UnitOfWork.ReviewRepository.add(review);
                }
                else
                {

                    review.Rate = rev.Rate;
                    UnitOfWork.ReviewRepository.update(review);
                }
                UnitOfWork.save();
                var rates = UnitOfWork.ReviewRepository.GetList(c => c.Rate > 0).ToList();
                var numOfStudent = rates.Count();
                var totalSumOfRates = rates.Sum(c => c.Rate);
                course.Rate = totalSumOfRates / numOfStudent;
                UnitOfWork.CourseRepository.update(course);
                UnitOfWork.save();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpGet("Getreviews/{id:int}")]
        public async Task<IActionResult> Getreviews(int id)
        {
            var review = UnitOfWork.ReviewRepository.GetList(r => r.CourseId == id, "Student").ToList();
            IEnumerable<ReviewDTO> Review = mapper.Map<IEnumerable<ReviewDTO>>(review).ToList();
            var reviewDict = review.ToDictionary(vs => vs.Id);
            foreach (var rev in Review)
            {
                if (reviewDict.TryGetValue(rev.Id, out var revv))
                {
                    rev.PhotoUrl = revv.Student.PictureUrl;
                    rev.Name = revv.Student.Fname + " " + revv.Student.Lname;
                }
            }
            return Ok(Review);

        }
    }
}
