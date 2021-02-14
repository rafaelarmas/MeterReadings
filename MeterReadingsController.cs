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
                // TODO: Is it better to accept a form with the file or just the file?
                //var formCollection = await Request.ReadFormAsync();
                //if (formCollection == null || formCollection.Count == 0) throw new Exception("Bad Request: File was not found.");
                //var file = formCollection.Files.First();

                if (Request?.Form?.Files.Count == 0) throw new Exception("Bad Request: File was not found.");

                var file = Request.Form.Files[0];
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
