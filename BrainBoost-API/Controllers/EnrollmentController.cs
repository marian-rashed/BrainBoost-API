using AutoMapper;
using BrainBoost_API.DTOs.Enrollment;
using BrainBoost_API.DTOs.Paylink;
using BrainBoost_API.DTOs.Subscription;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using HelperPlan.DTO.Paylink;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> user;

        // get section Paylink from appsettings.json by i options
        private readonly EnvironmentPaylink paylink;
        private readonly IMapper mapper;

        public EnrollmentController(IUnitOfWork unitOfWork, IOptions<EnvironmentPaylink> paylink, IMapper mapper, UserManager<ApplicationUser> user)
        {
            this.unitOfWork = unitOfWork;
            this.user = user;
            this.paylink = paylink.Value;
            this.mapper = mapper;
        }

        // Enroll Course
        [HttpPost("Enroll")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Enroll(EnrollmentDto enrollmentDto)
        {
            if (ModelState.IsValid)
            {

                string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                enrollmentDto.StudentId = unitOfWork.StudentRepository.Get(c => c.UserId == userId).Id;
                var orderNumber = Guid.NewGuid().ToString();

                var result = await GeneratePaylink(await AuthenticationPaylink(), enrollmentDto, orderNumber);

                var enrollment = mapper.Map<Enrollment>(enrollmentDto);
                enrollment.SubscribtionsStatus = result.OrderStatus;
                enrollment.TransactionNo = result.TransactionNo;
                enrollment.CheckUrl = result.CheckUrl;
                enrollment.orderNumber = orderNumber;
                this.unitOfWork.EnrollmentRepository.add(enrollment);
                this.unitOfWork.save();
                return Ok(new { Url = result.Url });

            }
            return BadRequest(ModelState);
        }

        // check Enrollment status
        [HttpGet("CheckStatus/{orderNumber}")]
        public async Task<IActionResult> CheckStatus(string? orderNumber)
        {
            var enrollment = this.unitOfWork.EnrollmentRepository.Get(x => x.orderNumber == orderNumber);
            StudentEnrolledCourses stdCourse = new StudentEnrolledCourses
            {
                CourseId = enrollment.CourseId,
                StudentId = enrollment.StudentId,
                Student = enrollment.Student,
                Course = enrollment.Course,
                QuizState = false,
                CertificateState = false
            };
            if (enrollment == null)
            {
                return NotFound();
            }
            var options = new RestClientOptions(this.paylink.url + $"/api/getInvoice/{enrollment.TransactionNo}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json;charset=UTF-8");
            var token = await AuthenticationPaylink();
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await client.GetAsync(request);
            //var rst = response.Content;
            var result = JsonConvert.DeserializeObject<GatewayOrderResponse>(response.Content);
            // using swatch to check status of subscribtion
            switch (result.OrderStatus)
            {
                case "Pending":
                    enrollment.SubscribtionsStatus = "Pending";
                    break;
                case "Paid":
                    enrollment.SubscribtionsStatus = "Paid";
                    enrollment.IsActive = true;
                    unitOfWork.StudentEnrolledCoursesRepository.add(stdCourse);
                    unitOfWork.save();
                    HandleVideoState(stdCourse.CourseId, stdCourse.StudentId , stdCourse.Id);
                    HandleEarnings(enrollment.Id);
                    break;
                case "Declined":
                    enrollment.SubscribtionsStatus = "Declined";
                    break;
                default:
                    enrollment.SubscribtionsStatus = "Cancelled ";
                    break;
            }

            this.unitOfWork.EnrollmentRepository.update(enrollment);
            this.unitOfWork.save();
            return Ok(result);
        }


        [NonAction]
        // generate paylink
        public async Task<GatewayOrderResponse> GeneratePaylink(string token, EnrollmentDto enrollmentDto, string orderNumber)
        {

            string studentId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentInfo = unitOfWork.StudentRepository.Get(c => c.UserId == studentId , "AppUser");
            var course = this.unitOfWork.CourseRepository.Get(x => x.Id == enrollmentDto.CourseId);

            var options = new RestClientOptions(this.paylink.url + "/api/addInvoice");
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("accept", "application/json");
            // add token to header
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(new
            {

                amount = course.Price,
                clientName = string.Format("{0} {1}", studentInfo.Fname, studentInfo.Lname),
                clientEmail = studentInfo.AppUser.Email,
                orderNumber = orderNumber,
                callBackUrl = $"http://localhost:4200/EnrollmentSuccess/{orderNumber}/{enrollmentDto.CourseId}",
                cancelUrl = $"http://localhost:4200/EnrollmentFailed/{orderNumber}/{enrollmentDto.CourseId}",
                currency = "SAR",
                note = "Test invoice",
                products = new List<object>
                {
                    new
                    {
                        title = course.Name,
                        price =course.Price,
                        qty = 1,
                        imageSrc = "",
                        description = "",
                        isDigital = false,
                        specificVat = 0,
                        productCost = 0,
                    }
                },
            });
            var response = await client.PostAsync(request);
            // deserialize response
            var rst = response.Content;
            var result = JsonConvert.DeserializeObject<GatewayOrderResponse>(response.Content);

            return result;
        }

        [NonAction]
        // Authentication paylink
        public async Task<string> AuthenticationPaylink()
        {
            var options = new RestClientOptions(this.paylink.url + "/api/auth");
            var client = new RestClient(options);
            var request = new RestRequest("");


            request.AddHeader("accept", "*/*");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(new
            {
                apiId = this.paylink.apiId,
                secretKey = this.paylink.secretKey,
                persistToken = this.paylink.persistToken,
            });
            var response = await client.PostAsync(request);
            var result = response.Content;
            // i need get id_token desrialize it and return it
            var token = JsonConvert.DeserializeObject<tokenPaylink>(result);
            // return token.id_token;
            return token.id_token;

        }

        [NonAction]

        public async Task HandleVideoState(int courseId , int studentId , int StudentEnrolledCourseId)
        {
            List<Video> videos = unitOfWork.VideoRepository.GetList(c => c.Course.Id == courseId ).ToList();
            foreach(Video video in videos)
            {
                VideoState videoState = new VideoState()
                {
                    VideoId = video.Id,
                    StudentEnrolledCourseId = StudentEnrolledCourseId,
                    State = false
                };
                unitOfWork.VideoStateRepository.add(videoState);
            }
            unitOfWork.save();
        }

        [HttpGet("CheckEnroll")]
        public async Task<IActionResult> CheckEnroll([FromQuery]int courseId , [FromQuery] string studentId)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userFromDb = await user.FindByIdAsync(studentId);
                if (userFromDb != null) {
                    var student = unitOfWork.StudentRepository.Get(S => S.AppUser.Id == userFromDb.Id);
                    var enrolledCourse = unitOfWork.StudentEnrolledCoursesRepository.Get(EC => EC.CourseId == courseId && EC.StudentId ==student.Id);
                    if (enrolledCourse != null)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return Ok(false);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [NonAction]

        public async Task HandleEarnings(int enrollmentId)
        {
            
            Enrollment enrollment =  unitOfWork.EnrollmentRepository.Get(E => E.Id == enrollmentId);

            Course currentCourse =  unitOfWork.CourseRepository.Get(C => C.Id == enrollment.CourseId);
            decimal amount = currentCourse.Price;
            decimal instructorEarnings = 0.8m * amount;
            decimal websiteEarnings = 0.2m * amount;
            Earnings earnings = new Earnings()
            {
                Amount = currentCourse.Price,
                enrollment = enrollment,
                enrollmentId = enrollment.Id,
                InstructorEarnings = instructorEarnings,
                paymentDate = DateTime.Now,
                IsDeleted = false,
                WebsiteEarnings = websiteEarnings
            };
            unitOfWork.EarningsRepository.add(earnings);
            unitOfWork.save();
                  
        }


    }
}
