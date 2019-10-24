using Microsoft.AspNetCore.Mvc;

namespace FSO.SDD.NativeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastBuildStatusController : ControllerBase
    {
        [HttpGet]
        public bool Get()
        {
            return false;
        }

        [HttpGet("{id}", Name = "Get")]
        public bool Get(int id)
        {
            return false;
        }
    }
}
