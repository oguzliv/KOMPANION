using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Application.Models.MovementModels.MovementResponses;
using Fitness.Domain.Entities;
using Fitness.Domain.Errors;
using Fitness.Infra.Repositories;
using Microsoft.AspNetCore.Http;

namespace Fitness.Application.Services.MovementService
{
    public class MovementService : IMovementService
    {
        private readonly MovementRepository _movementRepository;
        private readonly IMapper _mapper;
        private readonly Guid CurrentUserId;
        public MovementService(MovementRepository movementRepository, IMapper mapper, IHttpContextAccessor context)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
            CurrentUserId = Guid.Parse(context.HttpContext.User.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid).Value);
        }

        public async Task<Response> CreateMovement(CreateMovementRequest movementDto)
        {
            CreateMovementResponse response = new CreateMovementResponse();
            var movement = await _movementRepository.GetByName(movementDto.Name);

            if (movement != null)
            {
                response.Errors.Append(MovementError.MovementAlreadyExists);
                response.IsSuccess = false;
                return response;
            }

            movement = _mapper.Map<Movement>(movementDto);
            movement.Id = Guid.NewGuid();
            movement.CreatedAt = DateTime.UtcNow;
            movement.CreatedBy = CurrentUserId;

            await _movementRepository.Create(movement);

            response.Data = movement;
            response.IsSuccess = true;

            return response;
        }

        public async Task<bool> DeleteMovement(Guid id)
        {
            var movement = await _movementRepository.GetById(id);
            if (movement == null) return false;

            await _movementRepository.Delete(id);

            return true;

        }

        public async Task<Movement> GetMovementByName(string name)
        {
            return await _movementRepository.GetByName(name);
        }

        public async Task<IEnumerable<Movement>> GetMovements()
        {
            return await _movementRepository.Get();
        }

        public async Task<Response> UpdateMovement(UpdateMovementRequest movementUpdateDto)
        {
            UpdateMovementResponse response = new UpdateMovementResponse();
            var movement = await _movementRepository.GetById(movementUpdateDto.Id);

            if (movement == null)
            {
                response.Errors.Append(MovementError.MovementNotExists);
                response.IsSuccess = false;
                return response;
            }

            movement.Name = movementUpdateDto.Name;
            movement.MuscleGroup = movementUpdateDto.MuscleGroup.ToString();
            movement.UpdatedAt = DateTime.UtcNow;
            movement.UpdatedBy = CurrentUserId;

            await _movementRepository.Update(movement);

            response.Data = movement;
            response.IsSuccess = true;

            return response;
        }
    }
}