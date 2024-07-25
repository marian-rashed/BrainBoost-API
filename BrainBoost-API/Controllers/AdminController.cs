using AutoMapper;
using BrainBoost_API.DTOs.Admin;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        public AdminController(IUnitOfWork _unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            unitOfWork = _unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet("GetAllAdmins")]
        public IActionResult GetAllAdmins()
        {
            if (ModelState.IsValid)
            {
                List<Admin> admins = unitOfWork.AdminRepository.GetAll().ToList();
                List<AdminDTO> adminsData = new List<AdminDTO>();
                foreach (Admin admin in admins)
                {
                    AdminDTO adminData = mapper.Map<AdminDTO>(admin);
                    adminsData.Add(adminData);
                }
                return Ok(adminsData);
            }
            return BadRequest(ModelState);
        }


        [HttpGet("GetAdminById/{id}")]
        public IActionResult GetAdminById(int id)
        {
            Admin admin = unitOfWork.AdminRepository.Get(a => a.Id == id);

            if (admin == null)
            {
                return NotFound();
            }
            AdminDTO adminData = new AdminDTO();
            adminData = mapper.Map<AdminDTO>(admin);
            return Ok(adminData);
        }
        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            Admin admin = unitOfWork.AdminRepository.Get(a => a.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            ApplicationUser userFromDb = await userManager.FindByIdAsync(admin.UserId);
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

            admin.IsDeleted = true;
            unitOfWork.AdminRepository.remove(admin);
            unitOfWork.save();

            return Ok();
        }
        [HttpPut("UpdateAdminData")]
        public IActionResult UpdateAdminData(AdminDTO updatedAdmin)
        {
            Admin adminfromDB = unitOfWork.AdminRepository.Get(s => s.Id == updatedAdmin.Id);
            if (adminfromDB == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                mapper.Map(updatedAdmin, adminfromDB);
                unitOfWork.AdminRepository.update(adminfromDB);
                unitOfWork.save();
                return Ok();
            }
            return BadRequest(ModelState); ;
        }
        [HttpPost("uploadimage/{adminId:int}")]
        public async Task<IActionResult> UploadImage(IFormFile file, int adminId)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                { return BadRequest("No file uploaded."); }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Admin");
                var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);


                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var photoUrl = $"http://localhost:43827/Images/Admin/{fileName}";
                    Admin admin = unitOfWork.AdminRepository.Get(a => a.Id == adminId);
                    if (admin == null)
                    {
                        return NotFound("Admin not found.");
                    }
                    admin.PictureUrl = photoUrl;
                    unitOfWork.AdminRepository.update(admin);
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


    }
}
