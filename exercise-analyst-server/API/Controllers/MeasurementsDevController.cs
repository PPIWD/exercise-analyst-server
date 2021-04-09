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
            var result = await _measurementsDevService.CreateMeasurementAsync(request);

            return Ok("Gites");
        }
    }
}
