using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSO.SDD.NativeWebApi.Models.Grafana;

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
            return new string[] { "upper_25", "upper_50", "upper_75", "upper_90", "upper_95" };
        }

        /// <summary>
        /// Запрос данных по конкретным метрикам
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Query")]
        public string Query(QueryRequest request)
        {
            var result = GenerateRandomQueryAnswer(request);
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
        
  
        public IEnumerable<IQueryResponse> GenerateRandomQueryAnswer(QueryRequest request)
        {
            var result = new List<IQueryResponse>();

            foreach (var target in request.Targets)
            {
                if (target.Type == "timeseries")
                {
                    result.Add(GenerageRandomTimeseries(request, target.Target));
                }
                else if (target.Type == "table")
                {
                    result.Add(GenerageRandomTable(request, target.Target));
                }
            }
            return result;
        }

        
        public TimestampQueryResponse GenerageRandomTimeseries(QueryRequest request, string target)
        {
            
            var random = new Random();

            var timestamp = new TimestampQueryResponse()
            {
                Target = target
            };

            var list = new List<float[]>();
            for (var dt = request.Range.From; dt <= request.Range.To; dt = dt.AddMinutes(5))
            {
                var unixtime = dt.ToUnixTimeMilliseconds();
                list.Add(new float[] { Convert.ToSingle(random.NextDouble() * 1000), unixtime });
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
