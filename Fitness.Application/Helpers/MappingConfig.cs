using AutoMapper;
using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Application.Models.UserModels.UserRequest;
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
                // config.CreateMap<RegisterDto, User>().ForMember(
                //     dest => dest.Role,
                //     opt => opt.MapFrom(src => src.Role.ToString()));
                // config.CreateMap<User, UserDto>().ForMember(
                //     dest => dest.IsAdmin,
                //     opt => opt.MapFrom(src => src.Role == 0));
                // config.CreateMap<UserResultDto, User>().ForMember(
                //     dest => dest.RoleText,
                //     opt => opt.MapFrom(src => src.IsAdmin == true ? "ADMIN" : "CUSTOMER"));
                // config.CreateMap<User, UserResultDto>().ForMember(
                //     dest => dest.IsAdmin,
                //     opt => opt.MapFrom(src => src.Role == 0));
                // config.CreateMap<User, RegisterDto>().ForMember(
                //     dest => dest.IsAdmin,
                //     opt => opt.MapFrom(src => src.Role == 0));

                // config.CreateMap<RegisterDto, User>().ForMember(
                //     dest => dest.RoleText,
                //     opt => opt.MapFrom(src => src.IsAdmin == true ? "ADMIN" : "CUSTOMER"));

                // config.CreateMap<CustomerUpdateDto, User>().ReverseMap();
                // config.CreateMap<BookDto, Book>().ReverseMap();
                config.CreateMap<User, RegisterDto>().ReverseMap();
                config.CreateMap<MovementDto, Movement>().ForMember(
                    dest => dest.MuscleGroup,
                    opt => opt.MapFrom(src => src.MuscleGroup.ToString()));
                config.CreateMap<Movement, MovementDto>().ForMember(
                    dest => dest.MuscleGroup,
                    opt => opt.MapFrom(src => System.Enum.Parse(typeof(MuscleGroup), src.MuscleGroup)));
            });
            return mappingConfig;
        }
    }
}