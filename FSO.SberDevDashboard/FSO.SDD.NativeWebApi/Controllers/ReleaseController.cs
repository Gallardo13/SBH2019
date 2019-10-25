using FSO.SDD.DataBaseEfStore;
using FSO.SDD.NativeWebApi.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.SDD.NativeWebApi.Controllers
{
    public class TechnicalDebtInfo
    {
        public int ReleaseID { get; set; }
        public int Percent { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseController : HackController
    {
        public ReleaseController(StoreContext context, IMemoryCache cache) : base(context, cache) { }
        public ReleaseFacade facade = new ReleaseFacade();

        [Route("TechnicalDebt")]
        [HttpGet]
        public IEnumerable<TechnicalDebtInfo> GetTechnicalDebt()
        {
            return facade.GetTechnicalDebt(_context, _cache);
        }

        [Route("TechnicalDebt/{id}")]
        [HttpGet]
        public TechnicalDebtInfo GetTechnicalDebt(int id) => facade.GetTechnicalDebt(_context, id, _cache);

        [Route("TestCoverage")]
        [HttpGet]
        public IEnumerable<TechnicalDebtInfo> GetTestCoverage()
        {
            return facade.GetTestCoverage(_context, _cache);
        }

        [Route("TestCoverage/{id}")]
        [HttpGet]
        public TechnicalDebtInfo GetTestCoverage(int id) => facade.GetTestCoverage(_context, id, _cache);
    }
}
