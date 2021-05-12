using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using API.Domain.Models;
using API.Persistence;
using API.Services.Common;
using API.Services.Exercises.Dtos.Responses;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Exercises
{
    public class ExercisesService : IExercisesService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExercisesService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<GetExercisesResponse>> GetExercisesAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
                .Value;

            ApplicationUser user = null;

            if (userName != null)
                user = await _context.Users.FirstOrDefaultAsync(x => string.Equals(x.UserName.ToLower(), userName.ToLower()));


            if (user == null)
                return new Response<GetExercisesResponse>()
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new[] { "Nie znaleziono użytkownika (z JWT Token lub z parametru userId)" },
                };

            var exercises = await _context.Exercises
                .Where(x => x.UserId == user.Id)
                .ProjectTo<ExerciseForGetExercises>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var payload = new GetExercisesResponse
            {
                Exercises = exercises
            };

            var response = new Response<GetExercisesResponse>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Payload = payload
            };

            return response;
        }
    }
}
