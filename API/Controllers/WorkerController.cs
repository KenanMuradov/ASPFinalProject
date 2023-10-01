using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Worker")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
    }
}
