using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeterReadings.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingsController : ControllerBase
    {
        private readonly ILogger<MeterReadingsController> _logger;
        private readonly IMeterReadingsService _service;

        public MeterReadingsController(ILogger<MeterReadingsController> logger, IMeterReadingsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> MeterReadingUploads()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var result = await _service.ProcessFile(file);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error was found.");
                return BadRequest(e.Message);
            }
        }
    }
}
