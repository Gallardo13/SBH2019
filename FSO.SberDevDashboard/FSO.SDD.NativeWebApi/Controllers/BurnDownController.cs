using FSO.SDD.DataBaseEfStore;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public BurnDownController(StoreContext context) : base(context) { }

        // GET: api/BurnDown
        [HttpGet("{type}")]
        public BurnDownInfo Get(BurnDownType type)
        {
            var r = new Random((int)DateTime.Now.Ticks);

            var retVal = new BurnDownInfo
            {
                StartDate = DateTime.Now.AddDays(-5 * (int)type),
                EndDate = DateTime.Now.AddDays(5 * (int)type)
            };

            var days = new int[(retVal.EndDate - retVal.StartDate).Days + 1];
            var total = 100;
            days[0] = total;
            for (int i = 1; i < days.Length; i++)
            {
                days[i] = total -= 100 / (days.Length - 1) + r.Next(-1, 2);
                if (days[i] < 0)
                    days[i] = 0;
            }

            retVal.Days = days;

            return retVal;
        }
    }
}
