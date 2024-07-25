using AutoMapper;
using BrainBoost_API.DTOs.Category;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Mvc;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            if (ModelState.IsValid)
            {
                List<Category> categories = unitOfWork.CategoryRepository.GetAll().ToList();
                List<CategoryDTO> categoriesdata = new List<CategoryDTO>();
                foreach (Category category in categories)
                {
                    CategoryDTO categorydata = mapper.Map<CategoryDTO>(category);
                    categoriesdata.Add(categorydata);
                }
                return Ok(categoriesdata);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            Category category = unitOfWork.CategoryRepository.Get(a => a.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            CategoryDTO categoryData = new CategoryDTO();
            categoryData = mapper.Map<CategoryDTO>(category);
            return Ok(categoryData);
        }

        [HttpPost("addCategory")]
        public IActionResult addCategory(CategoryDTO newCategory)
        {
            if (ModelState.IsValid)
            {
                Category category = mapper.Map<Category>(newCategory);
                unitOfWork.CategoryRepository.add(category);
                unitOfWork.save();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category category = unitOfWork.CategoryRepository.Get(a => a.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsDeleted = true;
            unitOfWork.CategoryRepository.remove(category);
            unitOfWork.save();
            return Ok();
        }

        [HttpPut("UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            Category categoryfromDB = unitOfWork.CategoryRepository.Get(s => s.Id == id);
            if (categoryfromDB == null)
            {
                return NotFound();
            }

            if (categoryDTO.Id == id && ModelState.IsValid)
            {
                mapper.Map(categoryDTO, categoryfromDB);
                unitOfWork.CategoryRepository.update(categoryfromDB);
                unitOfWork.save();
                return Ok();
            }
            return BadRequest(ModelState);
        }

    }
}
