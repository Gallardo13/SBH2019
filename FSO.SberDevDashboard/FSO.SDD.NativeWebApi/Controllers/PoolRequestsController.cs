using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSO.SDD.NativeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolRequestsController : ControllerBase
    {
        [Route("confirmationtime")]
        [HttpGet]
        public IEnumerable<int> Get()
        {
            var r = new Random((int)DateTime.Now.Ticks);

            return new[] { r.Next(1, 5), r.Next(3, 7), r.Next(1, 2), r.Next(3, 7) };
        }
    }
}
