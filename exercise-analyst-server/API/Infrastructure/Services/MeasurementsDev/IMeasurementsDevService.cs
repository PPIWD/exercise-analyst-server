using System;
using System.Threading.Tasks;
using API.Dtos.Measurements.Requests;
using Microsoft.AspNetCore.Http;

namespace API.Infrastructure.Services.MeasurementsDev
{
    public interface IMeasurementsDevService
    {
        Task<Tuple<HttpResponse, string>> CreateMeasurementAsync(CreateMeasurementDevRequest request);
    }
}
