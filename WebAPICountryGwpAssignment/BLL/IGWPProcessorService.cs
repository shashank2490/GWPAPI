using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICountryGwpAssignment.BLL
{
    public interface IGWPProcessorService
    {
        Task<dynamic> GetAverageGWPForCountryLOBs(string country, string[] lobs);
    }
}
