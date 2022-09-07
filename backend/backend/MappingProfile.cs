using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDTO, User>().ForMember(entity => entity.UserName, 
                opt => opt.MapFrom(dto => dto.Email));
        }
    }
}
