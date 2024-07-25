using AutoMapper;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarningController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper mapper;
        public EarningController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.UserManager = userManager;
            this.UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet("GetTotalInstructorEarnings")]
        public ActionResult<decimal> GetTotalInstructorEarnings()
        {
            return UnitOfWork.EarningsRepository.GetTotalInstructorEarnings();
        }

        [HttpGet("GetTotalWebsiteEarnings")]
        public ActionResult<decimal> GetTotalWebsiteEarnings()
        {
            return UnitOfWork.EarningsRepository.GetTotalWebsiteEarnings();
        }

        [HttpGet("GetTotalEarning")]
        public IActionResult GetTotalEarning()
        {
            return Ok(UnitOfWork.EarningsRepository.GetTotalEarning());
        }

        [HttpGet("GetCoursesAndEarningsForInstructor/{instructorId:int}")]
        public IActionResult GetCoursesAndEarningsForInstructor(int instructorId)
        {
            return Ok(UnitOfWork.EarningsRepository.GetCoursesAndEarningsForInstructor(instructorId));
        }

        [HttpGet("GetTeachersAndEarnings")]
        public IActionResult GetTeachersAndEarnings()
        {
            return Ok(UnitOfWork.EarningsRepository.GetTeachersAndEarnings());
        }
    }
}
