using FSO.SDD.DataBaseEfStore;
using FSO.SDD.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSO.SDD.NativeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JiraTasksController : HackController
    {
        public JiraTasksController(StoreContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JiraTask>>> Get() =>
            await _context.JiraTasks.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<JiraTask>> Get(int id)
        {
            var jTask = await _context.JiraTasks.FindAsync(id);

            if (jTask == null)
                return NotFound();

            return jTask;
        }
    }
}
