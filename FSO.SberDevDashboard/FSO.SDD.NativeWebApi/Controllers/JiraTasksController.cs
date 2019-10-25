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
            Project = 1,
            Command = 2,
            Asignee = 3,
            ConfElement = 4
        }

        // задачи без чего либо
        [Route("WoPullRequest/{type}/{id}")]
        [HttpGet]
        public int GetWoPullRequest(WoType type, int id)
        {
            var r = new Random((int)DateTime.Now.Ticks);

            return r.Next(0, 1000) / 250;
        }

        public enum ByType
        {
            Sprint = 1,
            Epic = 2,
            Release = 3
        }

        public class ByStatusInfo
        {
            public string State { get; set; }
            public int Count { get; set; }
        }

        [Route("ByStatus/{type}/{id}")]
        [HttpGet]
        public IEnumerable<ByStatusInfo> GetByStatus(ByType type, int id)
        {
            var t = _context.JiraTasks.Include(e => e.State);

            switch (type)
            {
                case ByType.Sprint:
                    t.Where(e => _context.JiraSprintTasks.Where(s => s.SprintId == id).Select(s => s.TaskId).Contains(e.Id));
                    break;

                case ByType.Epic:
                    t.Where(e => _context.JiraEpicTasks.Where(s => s.EpicId == id).Select(s => s.EpicId).Contains(e.Id));
                    break;

                case ByType.Release:
                    t.Where(e => _context.JiraReleaseTasks.Where(s => s.ReleaseId == id).Select(s => s.ReleaseId).Contains(e.Id));
                    break;

                default:
                    return Enumerable.Empty<ByStatusInfo>();
            }

            return t.ToList()
                .GroupBy(e => e.State)
                .Select(e => new ByStatusInfo { State = e.Key.Name, Count = e.Count() });
        }
    }
}
