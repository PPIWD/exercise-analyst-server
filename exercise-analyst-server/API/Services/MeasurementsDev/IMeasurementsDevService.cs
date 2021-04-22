using System.Threading.Tasks;
using API.Services.Common;
using API.Services.MeasurementsDev.Dtos;
using API.Domain.Models;

namespace API.Services.MeasurementsDev
{
    public interface IMeasurementsDevService
    {
        Task<Response<int>> CreateMeasurementAsync(CreateMeasurementDevRequest request);
        Task<Response<Measurement>> GetMeasurementAsync(int id);
    }
}
