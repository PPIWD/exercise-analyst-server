using API.Services.Common;
using API.Services.Measurements.Dtos.Requests;

using System.Threading.Tasks;

namespace API.Infrastructure.MachineLearning
{
    public interface IMachineLearning
    {
        Task<Response<(string, int)>> Predict(CreateMeasurementRequest request);
    }
}
