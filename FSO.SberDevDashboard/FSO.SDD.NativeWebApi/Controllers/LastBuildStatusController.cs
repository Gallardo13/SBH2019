using Microsoft.AspNetCore.Mvc;

namespace FSO.SDD.NativeWebApi.Controllers
{
    public class BuildStatus
    {
        public bool Status { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LastBuildStatusController : ControllerBase
    {
        [HttpGet]
        public BuildStatus Get()
        {
            return new BuildStatus { Status = true };
        }

        [HttpGet("{id}")]
        public BuildStatus Get(int id)
        {
            return new BuildStatus { Status = false };
        }
    }
}
