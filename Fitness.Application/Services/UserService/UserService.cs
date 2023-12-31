using AutoMapper;
using Fitness.Application.Models.User;
using Fitness.Domain.Entites;
using Fitness.Domain.Errors;
using Fitness.Infra.Repositories;

namespace Fitness.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<object> CreateUser(RegisterDto user)
        {
            var _user = await _userRepository.GetByEmail(user.Email);

            if (_user == null)
            {
                _user = _mapper.Map<User>(user);
                _user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _user.Role = Domain.Enums.Role.User;
                _user.CreatedAt = DateTime.UtcNow;
                _user.Id = Guid.NewGuid();

                await _userRepository.Create(_user);

                return _user;
            }
            else
            {
                return UserErrors.UserAlreadyExists;
            }
        }
    }
}