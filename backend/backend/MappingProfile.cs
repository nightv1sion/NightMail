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

            CreateMap<MailDTO, Mail>();

            CreateMap<Mail, IncomingMailDTO>()
                .ForMember(dto => dto.SenderMail,
                opts => opts.MapFrom(entity => entity.Sender.Email));

            CreateMap<Mail, OutgoingMailDTO>()
                .ForMember(dto => dto.ReceiverMail,
                opts => opts.MapFrom(entity => entity.Receiver.Email));

            CreateMap<Folder, FolderDTO>()
                .ForMember(dto => dto.CountOfMails,
                opts => opts.MapFrom(entity => entity.MailFolders.Count()));
        }
    }
}
