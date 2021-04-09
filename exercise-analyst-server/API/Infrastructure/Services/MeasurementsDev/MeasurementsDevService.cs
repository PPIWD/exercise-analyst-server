using System;
using System.Threading.Tasks;
using API.Dtos.Measurements.Requests;
using Microsoft.AspNetCore.Http;

namespace API.Infrastructure.Services.MeasurementsDev
{
    public class MeasurementsDevService: IMeasurementsDevService
    {
        public Task<Tuple<HttpResponse, string>> CreateMeasurementAsync(CreateMeasurementDevRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
