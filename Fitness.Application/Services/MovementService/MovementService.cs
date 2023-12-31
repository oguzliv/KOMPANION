using AutoMapper;
using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Application.Models.MovementModels.MovementResponses;
using Fitness.Domain.Entities;
using Fitness.Domain.Errors;
using Fitness.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Application.Services.MovementService
{
    public class MovementService : IMovementService
    {
        private readonly MovementRepository _movementRepository;
        private readonly IMapper _mapper;
        public MovementService(MovementRepository movementRepository, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
        }

        public async Task<Response> CreateMovement(MovementDto movementDto)
        {
            CreateMovementResponse response = new CreateMovementResponse();
            var movement = await _movementRepository.GetByName(movementDto.Name);

            if (movement != null)
            {
                response.Errors.Append(MovementError.MovementAlreadyExists);
                response.IsSuccess = false;
            }

            movement = _mapper.Map<Movement>(movementDto);
            movement.Id = Guid.NewGuid();
            await _movementRepository.Create(movement);

            response.Data = movement;
            response.IsSuccess = true;

            return response;
        }

        public async Task<Movement> GetMovementByName(string name)
        {
            return await _movementRepository.GetByName(name);
        }

        public async Task<IEnumerable<Movement>> GetMovements()
        {
            return await _movementRepository.Get();
        }
    }
}