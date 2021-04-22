﻿using System.Net;
using System.Threading.Tasks;
using API.Services.MeasurementsDev;
using API.Services.MeasurementsDev.Dtos.Requests;
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
            var response = await _measurementsDevService.CreateMeasurementAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.NoContent)
                return NoContent();

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetMeasurementsDevCsv()
        {
            var response = await _measurementsDevService.GetMeasurementsCsvAsync();
            
            if (response.HttpStatusCode == HttpStatusCode.OK)
                return Ok(response);

            return BadRequest(response.Errors);
        }
    }
}
