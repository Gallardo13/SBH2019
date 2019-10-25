using FSO.SDD.DataBaseEfStore;
using Microsoft.AspNetCore.Mvc;
using System;
using FSO.SDD.NativeWebApi.Facades;
using Microsoft.Extensions.Caching.Memory;

namespace FSO.SDD.NativeWebApi.Controllers
{
    public enum BurnDownType
    {
        Spring = 1,
        Epic = 2,
        Release = 3
    }

    public class BurnDownInfo
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int[] Days { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BurnDownController : HackController
    {
        public BurnDownController(StoreContext context, IMemoryCache cache) : base(context, cache) { }

        // GET: api/BurnDown
        [HttpGet("{type}")]
        public BurnDownInfo Get(BurnDownType type)
        {
            var cacheKey = $"BurnDownController_{type}";
            if (_cache.TryGetValue(cacheKey, out BurnDownInfo val))
                return val;

            var startDate = DateTime.Now.AddDays(-5 * (int)type);
            var endDate = DateTime.Now.AddDays(5 * (int)type);

            val = new BurndownFacade().GetData(type, startDate, endDate);
            _cache.Set(cacheKey, val, new TimeSpan(0, 1, 0));
            return val;
        }
    }
}
