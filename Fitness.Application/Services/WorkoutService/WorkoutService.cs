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
            WorkoutCreateResponse response = new WorkoutCreateResponse();
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
            workout.Level = workoutDto.Level.ToString();
            workout.Duration = workoutDto.Duration.ToString();
            workout.Movements = workoutDto.Movements;

            await _workoutRepository.Create(workout);

            response.Data = workout;
            response.IsSuccess = true;

            return response;
        }

        public Task<bool> DeleteWorkout(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Workout>> GetWorkouts()
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateWorkout(UpdateWorkoutRequest WorkoutUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}