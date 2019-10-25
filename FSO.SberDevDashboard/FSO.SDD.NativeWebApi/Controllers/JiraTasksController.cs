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
            var t = _context.JiraTasks.Include(e => e.State).ToList();
            var t2 = new List<int>();

            switch (type)
            {
                case ByType.Sprint:
                    t2 = _context.JiraSprintTasks.Where(e => e.SprintId == id).Select(e => e.TaskId).ToList();
                    break;

                case ByType.Epic:
                    t2 = _context.JiraEpicTasks.Where(e => e.EpicId == id).Select(e => e.TaskId).ToList();
                    break;

                case ByType.Release:
                    t2 = _context.JiraReleaseTasks.Where(e => e.ReleaseId == id).Select(e => e.TaskId).ToList();
                    break;

                default:
                    return Enumerable.Empty<ByStatusInfo>();
            }

            return t.Where(e => t2.Contains(e.Id))
                .GroupBy(e => e.State)
                .Select(e => new ByStatusInfo { State = e.Key.Name, Count = e.Count() });
        }

        public class ByTypeInfo
        {
            public int DefectInPercent { get; set; }
            public int DefectsInStoryPoint { get; set; }
        }

        [Route("ByType/{type}")]
        [HttpGet]
        public IEnumerable<ByTypeInfo> GetByType(ByType type)
        {
            var retVal = new List<ByTypeInfo>();

            for (int i = 1; i < 16; i++)
                retVal.Add(GetByType(type, i));

            return retVal;
        }


        [Route("ByType/{type}/{id}")]
        [HttpGet]
        public ByTypeInfo GetByType(ByType type, int id)
        {
            var t = _context.JiraTasks.Include(e => e.State).ToList();
            var t2 = new List<int>();

            switch (type)
            {
                case ByType.Sprint:
                    t2 = _context.JiraSprintTasks.Where(e => e.SprintId == id).Select(e => e.TaskId).ToList();
                    break;

                case ByType.Release:
                    t2 = _context.JiraReleaseTasks.Where(e => e.ReleaseId == id).Select(e => e.TaskId).ToList();
                    break;

                default:
                    return new ByTypeInfo();
            }

            var retVal =
                t.Where(e => t2.Contains(e.Id)).Select(e => new { IsDefect = e.DefectSeverity > 0 ? true : false, e.OriginalEstimation })
                .GroupBy(e => e.IsDefect)
                .Select(e => new { IsDefect = e.Key, DefectInCount = e.Count(), DefectsInStoryPoint = e.Sum(t => t.OriginalEstimation) });

            var total = retVal.Sum(e => e.DefectInCount);

            try
            {
                return new ByTypeInfo
                {
                    DefectInPercent = (int)(retVal.Single(e => e.IsDefect).DefectInCount / (double)total * 100),
                    DefectsInStoryPoint = retVal.Single(e => e.IsDefect).DefectsInStoryPoint
                };
            }
            catch
            {
                return new ByTypeInfo();
            }
        }

        public class CriticalBugResolveInfo
        {
            public int SprintId { get; set; }
            public int Days { get; set; }
        }

        [Route("CriticalBugResolve")]
        [HttpGet]
        public IEnumerable<CriticalBugResolveInfo> CriticalBugResolve()
        {
            var retVal = new List<CriticalBugResolveInfo>();

            for (int i = 1; i < 16; i++)
                retVal.Add(CriticalBugResolve(i));

            return retVal;
        }


        [Route("CriticalBugResolve/{sprintId}")]
        [HttpGet]
        public CriticalBugResolveInfo CriticalBugResolve(int sprintId)
        {
            var t = _context.JiraTasks.Where(e => e.DefectSeverity > 0).ToList();
            var t2 = _context.JiraSprintTasks.Where(e => e.SprintId == sprintId).Select(e => e.TaskId).ToList();

            var r = new Random((int)DateTime.Now.Ticks);

            return new CriticalBugResolveInfo { SprintId = sprintId, Days = r.Next(3, 15) };
        }
    }
}
