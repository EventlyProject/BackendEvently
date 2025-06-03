using AutoMapper;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Evently.Shared.Mapping
{
    // AutoMapper profile for configuring object-object mappings between domain models and DTOs.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map User entity to UserDto
            CreateMap<User, UserDto>();

            // Map RegisterDto to User entity, but ignore PasswordHash (handled elsewhere)
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Map Event entity to EventDto
            CreateMap<Event, EventDto>()
                // Map CategoryName from the related Category entity (if present)
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(scr => scr.Category != null ? scr.Category.Name : string.Empty))
                // Map StartTime and LogoUrl directly (redundant, but explicit)
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl));

            // Map EventDto to Event entity
            CreateMap<EventDto, Event>()
                // Ignore Id to prevent overwriting primary key on updates
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                // Map StartTime and LogoUrl directly
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl));

            // Map Category entity to CategoryDto and vice versa (simple property mapping)
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            // Map EventPartipaint entity to ParticipationDto
            CreateMap<EventPartipaint, ParticipationDto>()
                // Map Username from the related User entity (if present)
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User != null ? src.User.Username : string.Empty))
                // Map EventName from the related Event entity (if present)
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event != null ? src.Event.Name : string.Empty));
        }
    }
}
