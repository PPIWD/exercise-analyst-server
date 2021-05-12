using API.Services.Common;
using API.Services.Measurements.Dtos.Requests;

using System.Threading.Tasks;

namespace API.Services.Measurements
{
    public interface IMeasurementsService
    {
        Task<Response> CreateMeasurementAsync(CreateMeasurementRequest request);
    }
}
