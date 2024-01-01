using Fitness.Application.Models.WorkoutModels.WorkoutRequests;
using Fitness.Application.Services.WorkoutService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Api.Controllers
{
    [Route("workouts")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateWorkoutRequest createWorkoutDto)
        {
            var result = await _workoutService.CreateWorkout(createWorkoutDto);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string muscleGroup, string duration, string level)
        {
            //TODO: pagination & filtering according to Duration,Level,MuscleGroup
            var result = await _workoutService.GetFilteredWorkouts(level, duration, muscleGroup);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _workoutService.GetWorkoutById(id);
            //TODO: pagination & filtering according to Duration,Level,MuscleGroup
            return Ok(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateWorkoutRequest updateWorkoutRequest)
        {
            var result = await _workoutService.UpdateWorkout(updateWorkoutRequest);
            return Ok(result);

        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _workoutService.DeleteWorkout(id);
            return Ok(result);

        }
    }
}