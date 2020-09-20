using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAPICountryGwpAssignment.BLL;
using WebAPICountryGwpAssignment.Models;

namespace WebAPICountryGwpAssignment.Controllers
{
    [Route("server/api/gwp")]
    [ApiController]
    public class CountryGwpController : ControllerBase
    {
        readonly ILogger _logger;
        readonly IGWPProcessorService _gwpProcessorService;
        public CountryGwpController(ILogger<CountryGwpController> logger, IGWPProcessorService gwpProcessorService)
        {
            _logger = logger;
            _gwpProcessorService = gwpProcessorService;
        }

        [HttpPost]
        [Route("avg")]
        public async Task<IActionResult> GetAvergaeGWPAcrossLOBs([FromBody] GWPRequestModel requestModel)
        {
            try
            {
                dynamic data = await _gwpProcessorService.GetAverageGWPForCountryLOBs(requestModel.Country, requestModel.Lobs);
                
                return Ok( JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while geeting LOB data");
                return StatusCode(500, "Some Error occured while getting LOB data");
            }
            

            
        }
    }
}
