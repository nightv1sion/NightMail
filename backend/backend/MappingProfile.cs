using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDTO, User>().ForMember(entity => entity.UserName, 
                opt => opt.MapFrom(dto => dto.Email));


            CreateMap<User, UserDTO>();

            CreateMap<User, UserForUpdateDTO>().ReverseMap();
            /*CreateMap<User, UserDTO>()
                .ForMember(entity => entity.Image, opts => opts.MapFrom(dto => dto.UserProfileImage.ToIFormFile()));*/
        }
    }
}
