using AutoMapper;
using PersonService.Models;
using PersonService.Resource;

namespace PersonService
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<ContactInfo, ContactInfoResource>();
             CreateMap<ContactType, ContactTypeResource>();
        }
    }
}