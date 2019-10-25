using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSO.SDD.NativeWebApi.Models.Grafana;
using FSO.SDD.NativeWebApi.Facades;

namespace FSO.SDD.NativeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrafanaController : ControllerBase
    {

        #region Grafana Api

        /// <summary>
        /// Стандартный запрос, вызывается графаной для проверки статуса готовности datasource
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Status()
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Запрос существующих метрик
        /// </summary>
        /// <returns></returns>
        [Route("search")]
        public IEnumerable<string> Search()
        {
            return new string[] { "Burndown спринта", "Burndown эпика", "Burndown релиза", "Оптимальный Burndown" };
        }

        /// <summary>
        /// Запрос данных по конкретным метрикам
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Query")]
        public string Query(QueryRequest request)
        {
            var result = ProcessRequest(request);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// Непонятный запрос, пока не вызывался 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Annotations")]
        public HttpResponseMessage Annotations(object request)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        #endregion

        #region Примеры генерации ответов
        
  
        public IEnumerable<IQueryResponse> ProcessRequest(QueryRequest request)
        {
            var result = new List<IQueryResponse>();

            foreach (var target in request.Targets)
            {

                if (target.Target == "Burndown спринта" || target.Target == "Burndown эпика" || target.Target == "Burndown релиза")
                {
                    var facade = new BurndownFacade();
                    var data = facade.GetData(BurnDownType.Spring, request.Range.From.DateTime, request.Range.To.DateTime);
                    result.Add(facade.ConvertBDItoTQR(data, target.Target));
                }

                if (target.Target == "Оптимальный Burndown")
                {
                    result.Add(GenerageOptimalBurndown(request, target.Target));
                }
                

                if (target.Type == "timeseries")
                {
                    //result.Add(GenerageRandomTimeseries(request, target.Target));
                }
                else if (target.Type == "table")
                {
                    //result.Add(GenerageRandomTable(request, target.Target));
                }
            }
            return result;
        }

        
        public TimestampQueryResponse GenerageOptimalBurndown(QueryRequest request, string target)
        {

            var total = 100;
            var step = total / (request.Range.To - request.Range.From).Days;


            var timestamp = new TimestampQueryResponse()
            {
                Target = target
            };

            var list = new List<long[]>();
            for (var dt = request.Range.From; dt <= request.Range.To; dt = dt.AddDays(1))
            {
                var unixtime = dt.ToUnixTimeMilliseconds();
                list.Add(new long[] { total, unixtime });
                total -= step;
            }

            timestamp.DataPoints = list.ToArray();

            return timestamp;
        }
        
        
        public TableQueryResponse GenerageRandomTable(QueryRequest request, string target)
        {
            var table = new TableQueryResponse()
            {
                Target = target,
                Type = "table"
            };

            table.Columns = new Column[] {
                new Column() { Text = "Field1", Type = "string"  },
                new Column() { Text = "Field2", Type = "number"  },
                new Column() { Text = "Field3", Type = "time"  },
            };

            table.Rows = new object[][]
            {
                new object[] { "F1", 10, DateTime.Now.AddMinutes(10) },
                new object[] { "F2", 20, DateTime.Now.AddMinutes(20) },
                new object[] { "F3", 30, DateTime.Now.AddMinutes(30) }
            };

            return table;
        }

        
        #endregion
    }
}
