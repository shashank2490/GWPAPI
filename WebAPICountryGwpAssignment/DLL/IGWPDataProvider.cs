using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICountryGwpAssignment.Models;

namespace WebAPICountryGwpAssignment.DLL
{
    public interface IGWPDataProvider
    {
        public Task<IEnumerable<GWPEntity>> GetCountryLOBsGWPDataForYearRange(string country, string[] lobs, int yearFrom, int yearTo);
    }
}
