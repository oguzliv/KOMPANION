using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.WorkoutModels.WorkoutRequests;
using Fitness.Application.Models.WorkoutModels.WorkoutResponses;
using Fitness.Domain.Entities;
using Fitness.Domain.Errors;
using Fitness.Infra.Repositories;
using Microsoft.AspNetCore.Http;

namespace Fitness.Application.Services.WorkoutService
{
    public class WorkoutService : IWorkoutService
    {
        private readonly WorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;
        private readonly Guid CurrentUserId;
        public WorkoutService(WorkoutRepository workoutRepository, IMapper mapper, IHttpContextAccessor context)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
            CurrentUserId = Guid.Parse(context.HttpContext.User.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid).Value);
        }
        public async Task<Response> CreateWorkout(CreateWorkoutRequest workoutDto)
        {
            CreateWorkoutResponse response = new CreateWorkoutResponse();
            var workout = await _workoutRepository.GetByName(workoutDto.Name);

            if (workout != null)
            {
                response.Errors.Append(WorkoutError.WorkoutAlreadyExists);
                response.IsSuccess = false;
                return response;
            }

            workout = _mapper.Map<Workout>(workoutDto);
            workout.Id = Guid.NewGuid();
            workout.CreatedAt = DateTime.UtcNow;
            workout.CreatedBy = CurrentUserId;
            // workout.Level = workoutDto.Level.ToString();
            // workout.Duration = workoutDto.Duration.ToString();
            // workout.workouts = workoutDto.workouts;

            await _workoutRepository.Create(workout);

            response.Data = workout;
            response.IsSuccess = true;

            return response;
        }

        public async Task<bool> DeleteWorkout(Guid id)
        {
            var workout = await _workoutRepository.GetById(id);
            if (workout == null) return false;

            await _workoutRepository.Delete(id);

            return true;
        }

        public Task<IEnumerable<Workout>> GetWorkouts()
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdateWorkout(UpdateWorkoutRequest workoutUpdateDto)
        {
            UpdateWorkoutResponse response = new UpdateWorkoutResponse();
            var workout = await _workoutRepository.GetById(workoutUpdateDto.Id);

            if (workout == null)
            {
                response.Errors.Append(WorkoutError.WorkoutNotExists);
                response.IsSuccess = false;
                return response;
            }

            workout.Name = workoutUpdateDto.Name;
            workout.Duration = workoutUpdateDto.Duration.ToString();
            workout.Level = workoutUpdateDto.Level.ToString();
            workout.Movements = string.Join(",", workoutUpdateDto.Movements.Select(id => id.ToString()));
            workout.UpdatedAt = DateTime.UtcNow;
            workout.UpdatedBy = CurrentUserId;

            await _workoutRepository.Update(workout);

            response.Data = workout;
            response.IsSuccess = true;

            return response;
        }
    }
}