using System;
using Newtonsoft.Json;

namespace FSO.SDD.NativeWebApi.Models.Grafana
{
    public class TableQueryResponse : IQueryResponse
    {
        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("columns")]
        public Column[] Columns { get; set; }

        [JsonProperty("rows")]
        public object[][] Rows { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Column
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
