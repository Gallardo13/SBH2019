using FSO.SDD.DataBaseEfStore;
using Microsoft.AspNetCore.Mvc;

namespace FSO.SDD.NativeWebApi.Controllers
{
    public class HackController : ControllerBase
    {
        protected readonly StoreContext _context;

        public HackController(StoreContext context)
        {
            _context = new StoreContext();
        }

    }
}
