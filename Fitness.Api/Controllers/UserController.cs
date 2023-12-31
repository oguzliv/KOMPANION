using Fitness.Application.Helpers;
using Fitness.Application.Models.UserModels.UserRequest;
using Fitness.Application.Services.UserService;
using Fitness.Application.Validators.UserValidators;
using Fitness.Domain.Abstractions;
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
        private readonly JwtTokenCreator _tokenCreator;
        public UserController(IUserService userService, RegisterDtoValidator registerDtoValidator, LoginDtoValidator loginDtoValidator, IConfiguration configuration, JwtTokenCreator tokenCreator)
        {
            _userService = userService;
            _registorDtoValidator = registerDtoValidator;
            _loginDtoValidator = loginDtoValidator;
            _configuration = configuration;
            _tokenCreator = tokenCreator;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var validate = _registorDtoValidator.Validate(registerDto);
            if (validate.IsValid)
            {
                var result = await _userService.CreateUser(registerDto);
                if (result.Errors.Any())
                    return BadRequest(result);
                return Ok(result);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var validate = _loginDtoValidator.Validate(loginDto);
            if (validate.IsValid)
            {
                var result = await _userService.GetUserByEmail(loginDto.Email);
                if (result == null)
                    return BadRequest(UserErrors.UserNotExists);
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, result!.Password))
                    return BadRequest(UserErrors.InvalidPassword);
                return Ok(_tokenCreator.TokenCreator(result, _configuration.GetSection("JWT:Secret")!.Value));
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }
    }
}