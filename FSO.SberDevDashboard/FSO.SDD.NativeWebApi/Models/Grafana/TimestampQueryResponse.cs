using System;
using Newtonsoft.Json;

namespace FSO.SDD.NativeWebApi.Models.Grafana
{
    public class TimestampQueryResponse : IQueryResponse
    {
        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("datapoints")]
        public long[][] DataPoints { get; set; }
    }
}
