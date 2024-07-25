using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public PlanController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Plan>> GetAll() {
            IEnumerable<Plan> allPlans = unitOfWork.PlanRepository.GetAll();
            return Ok(allPlans);
        }

        [HttpGet("GetById/{id:int}")]
        public ActionResult<Plan> Get(int id)
        {
            Plan plan = unitOfWork.PlanRepository.Get(P => P.Id == id);
            return Ok(plan);
        }

    }
}
