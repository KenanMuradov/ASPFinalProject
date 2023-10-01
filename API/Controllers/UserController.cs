using Application.Models.DTOs;
using Application.Models.DTOs.Category;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IClientService _service;

        public UserController(IClientService service)
        {
            _service = service;
        }

        [HttpGet("getAllWorkers")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<ActionResult<IEnumerable<WorkerDTO>>> GetAllWorkers() => Ok(await _service.GetAllWorkersAsync());

        [HttpGet("seeCategories")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public ActionResult<IEnumerable<CategoryShowDTO>> SeeAllCategories() => Ok(_service.SeeAllCategories());

        [HttpGet("getWorkersByRating")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<ActionResult<IEnumerable<WorkerDTO>>> GetWorkersByRating(bool Descending) => Ok(await _service.GetWorkersByRatingAsync(Descending));

        [HttpGet("getWorkersByCategory")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public ActionResult<IEnumerable<WorkerDTO>> GetWorkersByCategory(string categoryId) => Ok(_service.GetWorkersByCategory(categoryId));

        [HttpPost("sendWorkRequest")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<ActionResult<bool>> SendWorkRequest([FromBody] WorkRequestDTO request) => await _service.SendWorkRequest(request);
    }
}
