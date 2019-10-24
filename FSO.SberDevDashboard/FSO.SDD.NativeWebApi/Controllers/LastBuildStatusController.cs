using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FSO.SDD.NativeWebApi.Controllers
{
    public class BuildInfo
    {
        public int BuildNumber { get; set; }
        public string Status { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LastBuildStatusController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<BuildInfo> Get() =>
            JsonConvert
                .DeserializeObject<IEnumerable<BuildInfo>>(System.IO.File.ReadAllText(@"D:\Dropbox\Projects\Sber2019\SBH2019\FSO.SberDevDashboard\builds.json"));

        [HttpGet("{id}")]
        public BuildInfo Get(int id) =>
            JsonConvert
                .DeserializeObject<IEnumerable<BuildInfo>>(System.IO.File.ReadAllText(@"D:\Dropbox\Projects\Sber2019\SBH2019\FSO.SberDevDashboard\builds.json"))
                .FirstOrDefault(e => e.BuildNumber == id);
    }
}
