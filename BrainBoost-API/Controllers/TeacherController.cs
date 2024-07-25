using AutoMapper;
using BrainBoost_API.DTOs.Teacher;
using BrainBoost_API.Services.Uploader;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        public TeacherController(IUnitOfWork _unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            unitOfWork = _unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet("GetTeacherById/{id}")]
        public IActionResult GetTeacherById(int id)
        {
            Teacher teacher = unitOfWork.TeacherRepository.GetTeacherById(id);
            return Ok(teacher);
        }

        [HttpGet("GetCoursesOfTeacherById")]
        public IActionResult GetCoursesForTeacher(int TeacherId)
        {
            List<Course> Courses = unitOfWork.TeacherRepository.GetCoursesForTeacher(TeacherId);
            return Ok(Courses);
        }
        [HttpGet("GetCoursesCardsForTeacher")]
        public IActionResult GetCoursesCardsForTeacher(int TeacherId)
        {
            var Courses = unitOfWork.TeacherRepository.GetCoursesCardsForTeacher(TeacherId);
            return Ok(Courses);
        }
        [HttpGet("GetTeachers")]
        public async Task<IActionResult> GetTeachers()
        {
            if (ModelState.IsValid)
            {
                List<Teacher> teachers = unitOfWork.TeacherRepository.GetAll().ToList();
                List<TeacherAllDTO> teachersData = new List<TeacherAllDTO>();
                foreach (Teacher teacher in teachers)
                {
                    TeacherAllDTO teacherData = mapper.Map<TeacherAllDTO>(teacher);
                    teachersData.Add(teacherData);
                }
                return Ok(teachersData);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            Teacher teacher = unitOfWork.TeacherRepository.Get(t => t.Id == teacherId);
            if (teacher == null)
            {
                return NotFound();
            }

            ApplicationUser userFromDb = await userManager.FindByIdAsync(teacher.UserId);
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

            teacher.IsDeleted = true;
            unitOfWork.TeacherRepository.remove(teacher);
            unitOfWork.save();

            return Ok();
        }
        [HttpPut("UpdateTeacherData")]
        public async Task<IActionResult> UpdateTeacherData([FromForm]TeacherDataDTO updatedTeacher)
        {
            if (ModelState.IsValid)
            {
                //Teacher teacher = mapper.Map<Teacher>(updatedTeacher);
                Teacher teacher = unitOfWork.TeacherRepository.GetTeacherById(updatedTeacher.UserId);
                teacher.Career = updatedTeacher.Career;
                teacher.AboutYou = updatedTeacher.AboutYou;
                teacher.PhoneNumber = updatedTeacher.PhoneNumber;
                teacher.Address = updatedTeacher.Address;
                teacher.YearsOfExperience = updatedTeacher.YearsOfExperience;
                unitOfWork.TeacherRepository.update(teacher);
                if (updatedTeacher.Photo!=null)
                {
                  teacher.PictureUrl = await Uploader.uploadPhoto(updatedTeacher.Photo, teacher.GetType().Name, teacher.Fname+" "+teacher.Lname);
                }
                unitOfWork.save();
                return Ok(new {msg = "Updated succesfully"});
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetCoursesEarnings/{id}")]
        public async Task<IActionResult> GetCoursesEarnings(int id)
        {
            if (ModelState.IsValid)
            {
               
                return Ok("Successfully Updated");
            }
            return BadRequest(ModelState);
        }
        [HttpPost("uploadimage/{teacherId:int}")]
        public async Task<IActionResult> UploadImage(IFormFile file, int teacherId)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                { return BadRequest("No file uploaded."); }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Teacher");
                var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);


                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var photoUrl = $"http://localhost:43827/Images/Admin/{fileName}";
                    Teacher teacher = unitOfWork.TeacherRepository.Get(t => t.Id == teacherId);
                    if (teacher == null)
                    {
                        return NotFound("teacher not found.");
                    }
                    teacher.PictureUrl = photoUrl;
                    unitOfWork.TeacherRepository.update(teacher);
                    unitOfWork.save();
                    return Ok(photoUrl);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error: " + ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetTopTeachers")]
        public async Task<IActionResult> GetTopTeachers()
        {
            if (ModelState.IsValid)
            {
                List<Teacher> teachers = unitOfWork.TeacherRepository.GetTopTeachers();
                List<ModTeacherDTO> teachersData = new List<ModTeacherDTO>();
                foreach (Teacher teacher in teachers)
                {
                    ModTeacherDTO teacherData = mapper.Map<ModTeacherDTO>(teacher);
                    teachersData.Add(teacherData);
                }
                return Ok(teachersData);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetTotalNumOfTeachers")]
        public IActionResult GetTotalNumOfTeachers()
        {
            int numOfTeachers = unitOfWork.TeacherRepository.GetTotalNumOfTeachers();
            return Ok(numOfTeachers);
        }
    }
}
