using Fitness.Application.Models.User;
using Fitness.Application.Services.UserService;
using Fitness.Application.Validators.UserValidators;
using Fitness.Domain.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RegisterDtoValidator _registorDtoValidator;
        public UserController(IUserService userService, RegisterDtoValidator registerDtoValidator)
        {
            _userService = userService;
            _registorDtoValidator = registerDtoValidator;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var validate = _registorDtoValidator.Validate(registerDto);
            if (validate.IsValid)
            {
                var result = await _userService.CreateUser(registerDto);
                if (result is UserErrors)
                    return NotFound(result);
                return Ok(result);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }

        // [HttpPost("login")]
        // public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        // {

        // }

    }
}