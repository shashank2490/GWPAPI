using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICountryGwpAssignment.Models;

namespace WebAPICountryGwpAssignment.DLL
{
    public class CSVGWPDataProvider : IGWPDataProvider
    {
        readonly ILogger _logger;
        readonly IMemoryCache _cache;
        private List<GWPEntity> _cacheData;
        public CSVGWPDataProvider(ILogger<CSVGWPDataProvider> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
            
        }
        public Task<IEnumerable<GWPEntity>> GetCountryLOBsGWPDataForYearRange(string country, string[] lobs, int yearFrom, int yearTo)
        {
            try
            {
                Task<IEnumerable<GWPEntity>> task = new Task<IEnumerable<GWPEntity>>(() =>
                {
                    List<GWPEntity> cacheData = _cache.Get<List<GWPEntity>>("GWPData");

                    var filteredData = cacheData.Where(
                        x => x.Country == country && lobs.FirstOrDefault(y => y == x.LOB) != null && x.Year >= yearFrom && x.Year <= yearTo);

                    return filteredData;
                });

                task.Start();

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while querying and fetching data from csv file");
                return null;
            }
            
        }
    }
}
