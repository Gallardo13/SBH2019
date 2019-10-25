using FSO.SDD.DataBaseEfStore;
using Microsoft.AspNetCore.Mvc;
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
        public ReleaseController(StoreContext context) : base(context) { }

        [Route("TechnicalDebt")]
        [HttpGet]
        public IEnumerable<TechnicalDebtInfo> GetTechnicalDebt()
        {
            var r = new Random((int)DateTime.Now.Ticks);

            return _context.JiraReleases.Select(e => new TechnicalDebtInfo { ReleaseID = e.Id, Percent = r.Next(30, 90) });
        }

        [Route("TechnicalDebt/{id}")]
        [HttpGet]
        public TechnicalDebtInfo GetTechnicalDebt(int id) => GetTechnicalDebt().FirstOrDefault(e=>e.ReleaseID == id);

        [Route("TestCoverage")]
        [HttpGet]
        public IEnumerable<TechnicalDebtInfo> GetTestCoverage()
        {
            var r = new Random((int)DateTime.Now.Ticks);

            return _context.JiraReleases.Select(e => new TechnicalDebtInfo { ReleaseID = e.Id, Percent = r.Next(20, 25) });
        }

        [Route("TestCoverage/{id}")]
        [HttpGet]
        public TechnicalDebtInfo GetTestCoverage(int id) => GetTestCoverage().FirstOrDefault(e => e.ReleaseID == id);
    }
}
