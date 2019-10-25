using FSO.SDD.DataBaseEfStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FSO.SDD.NativeWebApi.Controllers
{
    public class HackController : ControllerBase
    {
        protected IMemoryCache _cache;
        protected readonly StoreContext _context;

        public HackController(StoreContext context, IMemoryCache cache)
        {
            _context = new StoreContext();
            _cache = cache;
        }

    }
}
