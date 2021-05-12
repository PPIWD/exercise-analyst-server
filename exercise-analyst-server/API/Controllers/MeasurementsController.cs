using System.Net;
using System.Threading.Tasks;

using API.Services.Measurements;
using API.Services.Measurements.Dtos.Requests;
using API.Services.MeasurementsDev;
using API.Services.MeasurementsDev.Dtos.Requests;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/measurements")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementsService _measurementsService;

        public MeasurementsController(IMeasurementsService measurementsService)
        {
            _measurementsService = measurementsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasurement([FromBody] CreateMeasurementRequest request)
        {
            var response = await _measurementsService.CreateMeasurementAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.Created)
                return Created("", "");

            return BadRequest(response.Errors);
        }
    }
}
