using System;
using System.Collections.Generic;
using System.Linq;
using FSO.SDD.DataBaseEfStore;
using FSO.SDD.NativeWebApi.Controllers;

namespace FSO.SDD.NativeWebApi.Facades
{
    public class ReleaseFacade
    {
        public ReleaseFacade()
        {

        }


        public IEnumerable<TechnicalDebtInfo> GetTechnicalDebt(StoreContext _context)
        {
            var r = new Random((int)DateTime.Now.Ticks);

            return _context.JiraReleases.Select(e => new TechnicalDebtInfo { ReleaseID = e.Id, Percent = r.Next(30, 90) });
        }

        public TechnicalDebtInfo GetTechnicalDebt(StoreContext _context, int id) => GetTechnicalDebt(_context).FirstOrDefault(e => e.ReleaseID == id);

        public IEnumerable<TechnicalDebtInfo> GetTestCoverage(StoreContext _context)
        {
            var r = new Random((int)DateTime.Now.Ticks);

            return _context.JiraReleases.Select(e => new TechnicalDebtInfo { ReleaseID = e.Id, Percent = r.Next(20, 25) });
        }

        public TechnicalDebtInfo GetTestCoverage(StoreContext _context, int id) => GetTestCoverage(_context).FirstOrDefault(e => e.ReleaseID == id);
    }
}
