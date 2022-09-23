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

            CreateMap<UserForUpdateDTO, User>()
                .ForMember(entity => entity.UserProfileImage, opts => opts.MapFrom(dto => dto.Image.ToUserProfileImage(dto)));

            CreateMap<User, UserDTO>();

            /*CreateMap<User, UserDTO>()
                .ForMember(entity => entity.Image, opts => opts.MapFrom(dto => dto.UserProfileImage.ToIFormFile()));*/
        }
    }
}
