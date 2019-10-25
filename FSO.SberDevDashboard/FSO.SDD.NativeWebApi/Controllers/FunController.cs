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
    public class FunController : ControllerBase
    {
        // GET: api/Fun
        [Route("co2/{days}")]
        [HttpGet]
        public IEnumerable<int> GetCo2Ppm(int days)
        {
            var r = new Random((int)DateTime.Now.Ticks);

            var retVal = new int[days];
            for (int i = 0; i < retVal.Length; i++)
                retVal[i] = r.Next(400, 1000);

            return retVal;
        }
    }
}
