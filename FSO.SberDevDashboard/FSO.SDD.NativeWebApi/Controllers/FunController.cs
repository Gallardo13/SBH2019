using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSO.SDD.DataBaseEfStore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FSO.SDD.NativeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunController : HackController
    {
        public FunController(StoreContext context, IMemoryCache cache) : base(context, cache) { }

        // GET: api/Fun
        [Route("co2/{days}")]
        [HttpGet]
        public IEnumerable<int> GetCo2Ppm(int days)
        {
            var cacheKey = $"FunController_GetCo2Ppm_{days}";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<int> val))
                return val;

            var r = new Random((int)DateTime.Now.Ticks);

            var retVal = new int[days];
            for (int i = 0; i < retVal.Length; i++)
                retVal[i] = r.Next(400, 1000);

            _cache.Set(cacheKey, retVal, new TimeSpan(0, 1, 0));

            return retVal;
        }
    }
}
