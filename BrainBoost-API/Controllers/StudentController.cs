using AutoMapper;
using BrainBoost_API.DTOs.Student;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            this.userManager = userManager;
        }
        //[HttpPost("AddStudent")]
        //public IActionResult AddStudent()
        //{
        //    Teacher teacher = new Teacher()
        //    {

        //    };
        //    return Ok("insertedTeacher");
        //}
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Student> students = UnitOfWork.StudentRepository.GetAll();
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }


        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Student student = UnitOfWork.StudentRepository.Get(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Student student = UnitOfWork.StudentRepository.Get(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            ApplicationUser userFromDb = await userManager.FindByIdAsync(student.UserId);
            if (userFromDb == null)
            {
                return NotFound();
            }

            userFromDb.IsDeleted = true;
            IdentityResult result = await userManager.UpdateAsync(userFromDb);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            student.IsDeleted = true;
            UnitOfWork.StudentRepository.remove(student);
            UnitOfWork.save();
            return Ok();
        }
        [HttpGet("GetTotalNumOfStudent")]
        public IActionResult GetTotalNumOfStudent()
        {
            int numofstudent = UnitOfWork.StudentRepository.GetTotalNumOfStudent();
            return Ok(numofstudent);
        }

        [HttpPut("Update")]
        public IActionResult UpdateStudent([FromForm] StudentDTO request)
        {
            var student = UnitOfWork.StudentRepository.Get(s => s.Id == request.Id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }
            student.Fname = request.Fname;
            student.Lname = request.Lname;
            UnitOfWork.StudentRepository.update(student);
            UnitOfWork.save();
            return Ok();

        }

        //public IActionResult Update(StudentDTO studentDTO, int id)
        //{
        //    Student studentfromDB = UnitOfWork.StudentRepository.Get(s => s.Id == id);
        //    if (studentfromDB == null)
        //    {
        //        return NotFound();
        //    }

        //    if (studentDTO.Id == id && ModelState.IsValid)
        //    {
        //        Mapper.Map(studentDTO, studentfromDB);
        //        UnitOfWork.StudentRepository.update(studentfromDB);
        //        UnitOfWork.save();
        //        return Ok("Data Updated Successfully");
        //    }
        //    return BadRequest(ModelState);

        //}
        [HttpPost("uploadimage/{studentId:int}")]
        public async Task<IActionResult> UploadImage(IFormFile file, int studentId)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                { return BadRequest("No file uploaded."); }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Student");
                var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);


                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var photoUrl = $"http://localhost:43827/Images/Student/{fileName}";
                    Student student = UnitOfWork.StudentRepository.Get(s => s.Id == studentId);
                    if (student == null)
                    {
                        return NotFound("student not found.");
                    }
                    student.PictureUrl = photoUrl;
                    UnitOfWork.StudentRepository.update(student);
                    UnitOfWork.save();
                    return Ok(photoUrl);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error: " + ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet("GetTopStudent")]
        public IActionResult GetTopStudent()
        {
            List<Student> students = UnitOfWork.StudentRepository.GetTopStudents();
            return Ok(students);
        }

        [HttpGet("GetTotalNumOfEnrolledCourses")]
        public IActionResult GetTotalNumOfEnrolledCourses()
        {
            int numofenrolledcourse = UnitOfWork.StudentRepository.GetTotalNumOfEnrolledCourses();
            return Ok(numofenrolledcourse);
        }
    }
}
