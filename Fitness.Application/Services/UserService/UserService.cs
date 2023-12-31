using System.Security.Principal;
using AutoMapper;
using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.UserModels.UserRequest;
using Fitness.Application.Models.UserModels.UserResponses;
using Fitness.Domain.Entites;
using Fitness.Domain.Errors;
using Fitness.Infra.Repositories;

namespace Fitness.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateUser(RegisterDto user)
        {
            LoginResponse response = new LoginResponse();
            var _user = await _userRepository.GetByEmail(user.Email);

            if (_user == null)
            {
                _user = _mapper.Map<User>(user);
                _user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _user.Role = Domain.Enums.Role.User;
                _user.CreatedAt = DateTime.UtcNow;
                _user.Id = Guid.NewGuid();

                await _userRepository.Create(_user);
                response.IsSuccess = true;
                response.Data = _user;
            }
            else
            {
                response.IsSuccess = false;
                response.Errors.Append(UserErrors.UserAlreadyExists);
            }

            return response;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }
    }
}