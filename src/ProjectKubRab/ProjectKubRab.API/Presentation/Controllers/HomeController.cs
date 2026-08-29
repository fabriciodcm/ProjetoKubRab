using Microsoft.AspNetCore.Mvc;
using ProjectKubRab.API.Core.Models.ViewModels;

namespace ProjectKubRab.API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/Home
        [HttpGet]
        public async Task<object> Get()
        {
            string? replicaName = Environment.GetEnvironmentVariable("KubPOD");
            return new { Message = "Welcome to the ProjectKubRab API", ReplicaName = replicaName };
        }
    }
}
