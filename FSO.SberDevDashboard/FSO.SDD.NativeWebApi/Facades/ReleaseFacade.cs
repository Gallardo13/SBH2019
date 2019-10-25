using FSO.SDD.DataBaseEfStore;
using FSO.SDD.NativeWebApi.Controllers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.SDD.NativeWebApi.Facades
{
    public class ReleaseFacade
    {
        public ReleaseFacade() { }

        public IEnumerable<TechnicalDebtInfo> GetTechnicalDebt(StoreContext _context, IMemoryCache _cache)
        {
            var cacheKey = $"ReleaseFacade_GetTechnicalDebt";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<TechnicalDebtInfo> val))
                return val;

            var r = new Random((int)DateTime.Now.Ticks);

            val = _context.JiraReleases.Select(e => new TechnicalDebtInfo { ReleaseID = e.Id, Percent = r.Next(30, 90) });
            _cache.Set(cacheKey, val, new TimeSpan(0, 1, 0));
            return val;
        }

        public TechnicalDebtInfo GetTechnicalDebt(StoreContext _context, int id, IMemoryCache _cache) => GetTechnicalDebt(_context, _cache).FirstOrDefault(e => e.ReleaseID == id);

        public IEnumerable<TechnicalDebtInfo> GetTestCoverage(StoreContext _context, IMemoryCache _cache)
        {
            var cacheKey = $"ReleaseFacade_GetTestCoverage";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<TechnicalDebtInfo> val))
                return val;

            var r = new Random((int)DateTime.Now.Ticks);

            val = _context.JiraReleases.Select(e => new TechnicalDebtInfo { ReleaseID = e.Id, Percent = r.Next(20, 25) });
            _cache.Set(cacheKey, val, new TimeSpan(0, 1, 0));
            return val;
        }

        public TechnicalDebtInfo GetTestCoverage(StoreContext _context, int id, IMemoryCache _cache) => GetTestCoverage(_context, _cache).FirstOrDefault(e => e.ReleaseID == id);
    }
}
