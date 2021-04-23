using System.Net;
using System.Threading.Tasks;
using API.Services.MeasurementsDev;
using API.Services.MeasurementsDev.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [Route("api/v{version:apiVersion}/measurements-dev")]
    [Route("api/measurements-dev")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class MeasurementsDevController: ControllerBase
    {
        private readonly IMeasurementsDevService _measurementsDevService;

        public MeasurementsDevController(IMeasurementsDevService measurementsDevService)
        {
            _measurementsDevService = measurementsDevService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasurementDev([FromBody] CreateMeasurementDevRequest request)
        {
            var response = await _measurementsDevService.CreateMeasurementAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.Created)
                return NoContent(); // api/v2 compatibility

            return BadRequest(response.Errors);
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> CreateMeasurementDev2_0([FromBody] CreateMeasurementDevRequest request)
        {
            var response = await _measurementsDevService.CreateMeasurementAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.Created)
                return Created($"api/measurements-dev/{response.Payload}",new {});

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetMeasurementsDev()
        {
            var response = await _measurementsDevService.GetMeasurementsAsync();
            return Ok(response);
        }
        
        [HttpGet("{measurementId}")]
        public async Task<IActionResult> GetMeasurementDev([FromRoute] int measurementId)
        {
            var response = await _measurementsDevService.GetMeasurementAsync(measurementId);
            
            if (response.HttpStatusCode == HttpStatusCode.OK)
                return Ok(response);

            return NotFound(response.Errors);
        }
    }
}
