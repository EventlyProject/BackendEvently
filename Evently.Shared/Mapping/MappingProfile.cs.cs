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
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // User
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>().
                ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Evet
            CreateMap<Event, EventDto>()
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(scr=>scr.Category != null?scr.Category.Name:string.Empty));

            CreateMap<CreateEventDto, Event>();

            // Category
            CreateMap<Category, CategoryDto>();

            // EventPartipaint
            CreateMap<EventPartipaint,ParticipationDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User != null ? src.User.Username : string.Empty))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event != null ? src.Event.Name : string.Empty));
        }
    }
}
