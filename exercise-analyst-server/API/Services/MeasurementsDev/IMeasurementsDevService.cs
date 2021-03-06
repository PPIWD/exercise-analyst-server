using System.Threading.Tasks;
using API.Services.Common;
using API.Services.MeasurementsDev.Dtos.Requests;
using API.Services.MeasurementsDev.Dtos.Responses;

namespace API.Services.MeasurementsDev
{
    public interface IMeasurementsDevService
    {
        Task<Response<int>> CreateMeasurementAsync(CreateMeasurementDevRequest request);
        Task<Response<GetMeasurementsResponse>> GetMeasurementsAsync();
        Task<Response<GetMeasurementResponse>> GetMeasurementAsync(int measurementId);
    }
}
