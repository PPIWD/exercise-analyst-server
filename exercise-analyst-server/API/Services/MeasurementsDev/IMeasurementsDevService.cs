using System.Threading.Tasks;
using API.Services.Common;
using API.Services.MeasurementsDev.Dtos.Requests;

namespace API.Services.MeasurementsDev
{
    public interface IMeasurementsDevService
    {
        Task<Response> CreateMeasurementAsync(CreateMeasurementDevRequest request);
    }
}
