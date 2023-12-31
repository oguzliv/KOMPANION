using Fitness.Application.Helpers;
using Fitness.Application.Models.UserModels;
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
        private readonly LoginDtoValidator _loginDtoValidator;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, RegisterDtoValidator registerDtoValidator, LoginDtoValidator loginDtoValidator, IConfiguration configuration)
        {
            _userService = userService;
            _registorDtoValidator = registerDtoValidator;
            _loginDtoValidator = loginDtoValidator;
            _configuration = configuration;
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

        [HttpPost("register")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var validate = _loginDtoValidator.Validate(loginDto);
            if (validate.IsValid)
            {
                var result = await _userService.GetUserByEmail(loginDto.Email);
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, result!.Password))
                    return NotFound(result);
                return Ok(JwtTokenCreator.TokenCreator(result, _configuration));
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }
    }
}