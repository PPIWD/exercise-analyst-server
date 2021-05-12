using System.Threading.Tasks;
using API.Services.Common;
using API.Services.Exercises.Dtos.Responses;

namespace API.Services.Exercises
{
    public interface IExercisesService
    {
        Task<Response<GetExercisesResponse>> GetExercisesAsync();
    }
}
