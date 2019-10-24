using FSO.SDD.DataBaseEfStore;
using FSO.SDD.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public enum WoType
        {
            PullRequest = 1,
            Project = 2,
            Command = 3,
            Asignee = 4,
            ConfElement = 5
        }

        // задачи без чего либо
        [Route("WoPullRequest")]
        [HttpGet("{type}/{id}")]
        public IEnumerable<int> GetWoPullRequest(WoType type, int id)
        {
            var r = new Random((int)DateTime.Now.Ticks);

            return new int[]
            {
                r.Next(0, 1000) / 350, // без пулл-реквеста
                r.Next(0, 1000) / 350, // без проекта
                r.Next(0, 1000) / 350, // без команды
                r.Next(0, 1000) / 350, // без сотрудника
                r.Next(0, 1000) / 350, // без КЭ
            };
        }
    }
}
