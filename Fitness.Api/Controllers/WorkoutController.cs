using Fitness.Application.Models.WorkoutModels.WorkoutRequests;
using Fitness.Application.Services.WorkoutService;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Api.Controllers
{
    [Route("workouts")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutService _workoutService;

        public WorkoutController(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateWorkoutRequest createWorkoutDto)
        {
            var result = await _workoutService.Create(createWorkoutDto);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //TODO: pagination & filtering according to Duration,Level,MuscleGroup
            return Ok();
        }
    }
}