using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Application.Models.MovementModels.MovementResponses;
using Fitness.Application.Services.MovementService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Api.Controllers
{
    [Route("movements")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService _movementService;
        public MovementController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var result = await _movementService.GetMovements();
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] MovementDto movementDto)
        {
            var result = await _movementService.CreateMovement(movementDto);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] MovementUpdateDto movementUpdateDto)
        {
            var result = await _movementService.UpdateMovement(movementUpdateDto);
            return Ok(result);
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