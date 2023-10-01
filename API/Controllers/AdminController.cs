using Application.Models;
using Application.Models.DTOs;
using Application.Models.DTOs.Category;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
       private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpPost("addCategory")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult<bool>> AddCategory([FromBody] CategoryDTO model) => await _service.AddCategoryAsync(model);


        [HttpGet("showAllCategories")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public ActionResult<IEnumerable<CategoryShowDTO>> GetAllCategories() => Ok(_service.GetAllCategories());

        [HttpPut("updateCategory")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult<bool>> UpdateCategory([FromBody] CategoryUpdateDTO model) => await _service.UpdateCategoryAsync(model);

        [HttpPost("addAdmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult<bool>> AddAdmin([FromBody] LoginRequest model) => await _service.AddNewAdmin(model);

        [HttpGet("getStatistics")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public ActionResult<Statistics> GetStatistics() => _service.GetStatistics();
    }
}
