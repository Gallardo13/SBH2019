using System;
using System.Collections.Generic;
using FSO.SDD.NativeWebApi.Controllers;
using FSO.SDD.NativeWebApi.Models.Grafana;
using Microsoft.Extensions.Caching.Memory;

namespace FSO.SDD.NativeWebApi.Facades
{
    public class BurndownFacade
    {
        public BurndownFacade()
        {
            
        }

        public BurnDownInfo GetData(BurnDownType type, DateTime startDate, DateTime endDate, IMemoryCache _cache)
        {
            var cacheKey = $"BurnDownController_{type}";
            if (_cache.TryGetValue(cacheKey, out BurnDownInfo val))
                return val;

            var r = new Random((int)DateTime.Now.Ticks);

            var retVal = new BurnDownInfo
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var days = new int[(retVal.EndDate - retVal.StartDate).Days + 1];
            var total = 100;
            days[0] = total;
            for (int i = 1; i < days.Length; i++)
            {
                days[i] = total -= 100 / (days.Length - 1) + r.Next(-1, 2);
                if (days[i] < 0)
                    days[i] = 0;
            }

            retVal.Days = days;

            _cache.Set(cacheKey, retVal, new TimeSpan(0, 1, 0));
            return retVal;
        }

        public TimestampQueryResponse ConvertBDItoTQR(BurnDownInfo data, string target)
        {

            var timestamp = new TimestampQueryResponse()
            {
                Target = target
            };

            var list = new List<long[]>();

            var i = 0;
            for (var dt = data.StartDate; dt <= data.EndDate; dt = dt.AddMinutes(5))
            {
                var unixtime = DateTimeOffset.Parse(dt.ToString()).ToUnixTimeMilliseconds();

                list.Add(new long[] { data.Days[i], unixtime });
                if (dt.Day != dt.AddMinutes(5).Day) i++;
            }

            timestamp.DataPoints = list.ToArray();

            return timestamp;
        }
    }
}
