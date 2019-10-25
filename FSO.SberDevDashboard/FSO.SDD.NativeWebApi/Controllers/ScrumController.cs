using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSO.SDD.NativeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrumController : ControllerBase
    {
        [Route("CommandFeeling/{days}")]
        [HttpGet]
        public IEnumerable<string> CommandFeeling(int days)
        {
            var r = new Random((int)DateTime.Now.Ticks);

            var retVal = new string[days];
            for (int i = 0; i < retVal.Length; i++)
            {
                var v = r.Next(1, 100);
                retVal[i] = v > 50 ? "Good" : v > 20 ? "Normal" : "Bad";
            }

            return retVal;
        }

        public enum CeremonyType
        {
            Daily = 1,
            Planning = 2,
            Retrospective = 3,
            SprintReview = 4
        }

        [Route("CeremonialTiming/{ceremonyType}/{days}")]
        [HttpGet]
        public IEnumerable<int> CeremonialTiming(CeremonyType ceremonyType, int days)
        {
            int maxTime = 0;

            switch (ceremonyType)
            {
                case CeremonyType.Daily: maxTime = 15; break;
                case CeremonyType.Planning: maxTime = 120; break;
                case CeremonyType.Retrospective: maxTime = 60; break;
                case CeremonyType.SprintReview: maxTime = 120; break;
                default:
                    break;
            }

            var r = new Random((int)DateTime.Now.Ticks);

            var retVal = new int[days];
            for (int i = 0; i < retVal.Length; i++)
                retVal[i] = r.Next(1, maxTime + r.Next(-maxTime / 10, maxTime / 10));

            return retVal;
        }
    }
}