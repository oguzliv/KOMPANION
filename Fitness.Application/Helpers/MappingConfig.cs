using AutoMapper;
using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Application.Models.UserModels.UserRequest;
using Fitness.Application.Models.WorkoutModels.WorkoutRequests;
using Fitness.Domain.Entites;
using Fitness.Domain.Entities;
using Fitness.Domain.Enums;

namespace Fitness.Application.Helpers
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<User, RegisterDto>().ReverseMap();
                config.CreateMap<CreateWorkoutRequest, Workout>().ForMember(
                    dest => dest.Movements,
                    opt => opt.MapFrom(src => string.Join(",", src.Movements.Select(id => id.ToString())))
                );
                config.CreateMap<Movement, CreateMovementRequest>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}