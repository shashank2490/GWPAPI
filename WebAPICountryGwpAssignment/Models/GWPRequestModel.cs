using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICountryGwpAssignment.Models
{
    public class GWPRequestModel
    {
        public string Country { get; set; }
        public string[] Lobs { get; set; }
    }
}
