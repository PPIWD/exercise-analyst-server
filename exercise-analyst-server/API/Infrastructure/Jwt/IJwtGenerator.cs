using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Infrastructure.Jwt
{
    public interface IJwtGenerator
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }
}