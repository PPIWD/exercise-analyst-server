using System.Threading.Tasks;
using API.Services.Auth.Dtos;
using API.Services.Auth.Dtos.Requests;
using API.Services.Auth.Dtos.Responses;
using API.Services.Common;

namespace API.Services.Auth
{
    public interface IAuthService
    {
        Task<Response<LoginResponse>> LoginAsync(LoginRequest request);
        Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request);
    }
}