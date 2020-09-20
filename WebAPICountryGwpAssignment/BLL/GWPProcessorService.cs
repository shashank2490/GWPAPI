using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICountryGwpAssignment.DLL;

namespace WebAPICountryGwpAssignment.BLL
{
    public class GWPProcessorService : IGWPProcessorService
    {
        private IMemoryCache _cache;
        readonly ILogger _logger;
        readonly IGWPDataProvider _gwpDataProvider;
        public GWPProcessorService(ILogger<GWPProcessorService> logger, IGWPDataProvider gwpDataProvider, IMemoryCache cache)
        {
            _logger = logger;
            _gwpDataProvider = gwpDataProvider;
            _cache = cache;
        }

        public async Task<dynamic> GetAverageGWPForCountryLOBs(string country, string[] lobs)
        {
            Dictionary<string, decimal> aggregatedData = new Dictionary<string, decimal>();
            try
            {
                Array.Sort(lobs);

                var cacheKey = $"{country}-{string.Join(',', lobs)}";

                if (_cache.TryGetValue(cacheKey, out aggregatedData))
                {
                    return aggregatedData;
                }
                else
                {
                    var results = await _gwpDataProvider.GetCountryLOBsGWPDataForYearRange(country, lobs, 2008, 2015);

                    if (results != null)
                    {
                        //var aggregatedData = results.GroupBy(x => x.LOB).Select(y => new { LOB=y.Key, AverageGWP = y.Select(x=>x.GWP).Average() });

                        aggregatedData = results.GroupBy(x => x.LOB).ToDictionary(y => y.Key, y => y.Select(x => x.GWP).Average());

                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));

                        _cache.Set(cacheKey, aggregatedData);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Inside GetAverageGWPForCountryLOBs: Error while aggregating Country LOB GWP.");
            }

            return aggregatedData;
        }
    }
}
