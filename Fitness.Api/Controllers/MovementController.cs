using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Application.Models.MovementModels.MovementResponses;
using Fitness.Application.Services.MovementService;
using Fitness.Application.Validators.UserValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Api.Controllers
{
    [Route("movements")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService _movementService;
        private readonly CreateMovementRequestValidator _createValidator;
        private readonly UpdateMovementRequestValidator _updateValidator;
        public MovementController(IMovementService movementService, CreateMovementRequestValidator createValidator, UpdateMovementRequestValidator updateValidator)
        {
            _movementService = movementService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var result = await _movementService.GetMovements();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _movementService.GetMovementById(id);
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] CreateMovementRequest movementDto)
        {
            var validate = _createValidator.Validate(movementDto);
            if (validate.IsValid)
            {
                var result = await _movementService.CreateMovement(movementDto);
                return Ok(result);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateMovementRequest movementUpdateDto)
        {
            var validate = _updateValidator.Validate(movementUpdateDto);
            if (validate.IsValid)
            {
                var result = await _movementService.UpdateMovement(movementUpdateDto);
                return Ok(result);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _movementService.DeleteMovement(id);
            return Ok(result);
        }
    }
}