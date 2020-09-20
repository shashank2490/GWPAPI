using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICountryGwpAssignment.Models
{
    public class GWPEntity
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string LOB { get; set; }

        public int Year { get; set; }
        public decimal GWP { get; set; }
    }
}
