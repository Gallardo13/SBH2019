using System;
using Newtonsoft.Json;

namespace FSO.SDD.NativeWebApi.Models.Grafana
{
      public class QueryRequest
    {
        [JsonProperty("panelId")]
        public long PanelId { get; set; }

        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("rangeRaw")]
        public Raw RangeRaw { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("intervalMs")]
        public long IntervalMs { get; set; }

        [JsonProperty("maxDataPoints")]
        public long MaxDataPoints { get; set; }

        [JsonProperty("targets")]
        public QueryTarget[] Targets { get; set; }

        [JsonProperty("adhocFilters")]
        public AdhocFilter[] AdhocFilters { get; set; }
    }

    public class AdhocFilter
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Range
    {
        [JsonProperty("from")]
        public DateTimeOffset From { get; set; }

        [JsonProperty("to")]
        public DateTimeOffset To { get; set; }

        [JsonProperty("raw")]
        public Raw Raw { get; set; }
    }

    public class Raw
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    public class QueryTarget
    {
        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("refId")]
        public string RefId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("additional")]
        public string Additional { get; set; }
    }
}
