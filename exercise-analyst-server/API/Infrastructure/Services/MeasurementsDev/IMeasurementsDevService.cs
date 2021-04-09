using System;
using System.Net;
using System.Threading.Tasks;
using API.Infrastructure.Services.MeasurementsDev.Dtos.Requests;
using Microsoft.AspNetCore.Http;

namespace API.Infrastructure.Services.MeasurementsDev
{
    public interface IMeasurementsDevService
    {
        Task<Tuple<HttpStatusCode, string>> CreateMeasurementAsync(CreateMeasurementDevRequest request);
    }
}
