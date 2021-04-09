using System;
using System.Net;
using System.Threading.Tasks;
using API.Infrastructure.Services.MeasurementsDev;
using API.Infrastructure.Services.MeasurementsDev.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/measurements-dev")]
    [ApiController]
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
            var (httpStatusCode, message) = await _measurementsDevService.CreateMeasurementAsync(request);

            if (httpStatusCode == HttpStatusCode.Created)
                return Created("", "");

            return BadRequest(message);
        }
    }
}
