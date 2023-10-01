using Application.Models.DTOs;
using Application.Models.DTOs.Worker;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _service;

        public WorkerController(IWorkerService service)
        {
            _service = service;
        }

        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Worker")]
        public async Task<ActionResult<ProfileDTO>> GetProfile(string email) => Ok(await _service.GetWorkerProfile(email));

        [HttpGet("seeActiveReuests")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Worker")]
        public ActionResult<IEnumerable<RequestDTO>> GetActiveReuests(string email) => Ok(_service.SeeActiveRequests(email));

        [HttpGet("seeInactiveReuests")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Worker")]
        public ActionResult<IEnumerable<RequestDTO>> GetInactiveReuests(string email) => Ok(_service.SeeInactiveRequests(email));

        [HttpGet("seeCompletedReuests")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Worker")]
        public ActionResult<IEnumerable<RequestDTO>> GetCompletedReuests(string email) => Ok(_service.SeeCompletedTasks(email));

        [HttpPost("completeTask")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Worker")]
        public async Task<ActionResult<bool>> CompleteTask([FromBody] SetWorkDoneRequest request) => Ok(await _service.SetWorkDoneAsync(request));

        [HttpPost("acceptTask")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Worker")]
        public async Task<ActionResult<bool>> AcceptTask([FromBody] AcceptWorkRequest request) => Ok(await _service.AcceptWorkAsync(request));

        [HttpPost("rejectTask")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Worker")]
        public async Task<ActionResult<bool>> RejectTask([FromBody] RejectWorkRequest request) => Ok(await _service.RejectWorkAsync(request));
    }
}
