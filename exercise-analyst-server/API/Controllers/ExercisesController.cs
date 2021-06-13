using System.Net;
using System.Threading.Tasks;

using API.Services.Exercises;
using API.Services.Exercises.Dtos.Responses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/exercises")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExercisesService _exercisesService;

        public ExercisesController(IExercisesService exercisesService)
        {
            _exercisesService = exercisesService;
        }

        [Produces(typeof(GetExercisesResponse))]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetExercises()
        {
            var response = await _exercisesService.GetExercisesAsync();

            if (response.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound(response.Errors);

            if(response.HttpStatusCode == HttpStatusCode.OK)
                return Ok(response);

            return BadRequest();
        }
    }
}
