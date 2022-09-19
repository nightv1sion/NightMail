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

            CreateMap<User, UserDTO>();
            CreateMap<Mail, MailDTO>()
                .ForMember(mDto => mDto.Sender, opt => opt.MapFrom(m => m.Sender))
                .ForMember(mDto => mDto.Receiver, opt => opt.MapFrom(m => m.Receiver));
        }
    }
}
