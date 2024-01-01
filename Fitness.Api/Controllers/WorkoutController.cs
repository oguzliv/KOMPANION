using Fitness.Application.Models.WorkoutModels.WorkoutRequests;
using Fitness.Application.Services.WorkoutService;
using Fitness.Application.Validators.UserValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Api.Controllers
{
    [Route("workouts")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly CreateWorkoutRequestValidator _createValidator;
        private readonly UpdateWorkoutRequestValidator _updateValidtor;

        public WorkoutController(IWorkoutService workoutService, CreateWorkoutRequestValidator createValidtor, UpdateWorkoutRequestValidator updateValidtor)
        {
            _workoutService = workoutService;
            _createValidator = createValidtor;
            _updateValidtor = updateValidtor;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateWorkoutRequest createWorkoutDto)
        {
            var validate = _createValidator.Validate(createWorkoutDto);
            if (validate.IsValid)
            {
                var result = await _workoutService.CreateWorkout(createWorkoutDto);
                return Ok(result);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string muscleGroup, string duration, string level)
        {
            var result = await _workoutService.GetFilteredWorkouts(level, duration, muscleGroup);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _workoutService.GetWorkoutById(id);
            return Ok(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateWorkoutRequest updateWorkoutRequest)
        {
            var validate = _updateValidtor.Validate(updateWorkoutRequest);
            if (validate.IsValid)
            {
                var result = await _workoutService.UpdateWorkout(updateWorkoutRequest);
                return Ok(result);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }

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