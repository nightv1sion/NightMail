using AutoMapper;
using Contracts;
using Entities.ExceptionModels;
using Entities.Models;
using Services.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MailService : IMailService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IMailFolderService _mailFolderService;

        public MailService(IRepositoryManager repository, IMapper mapper, IMailFolderService mailFolderService)
        {
            _repository = repository;
            _mapper = mapper;
            _mailFolderService = mailFolderService;
        }

        public async Task CreateMailAsync(Guid senderId, MailForCreateDTO mailDto)
        {
            var sender = GetUserAndIfItExists(senderId, false);

            var receiver = _repository.User.GetUserByEmail(mailDto.ReceiverMail, false);

            if (receiver == null)
                throw new UserNotFoundException(mailDto.ReceiverMail);

            var mail = _mapper.Map<Mail>(mailDto);

            mail.SenderId = sender.Id;
            mail.ReceiverId = receiver.Id;

            _repository.Mail.CreateMail(mail);
            await _repository.SaveAsync();
        }

        public async Task<List<MailDTO>> GetIncomingMailsAsync(Guid userId, bool trackChanges)
        {
            var user = GetUserAndIfItExists(userId, trackChanges);

            var mailEntities = await _repository.Mail.GetIncomingMailsForUserAsync(user, trackChanges);

            var mails = _mapper.Map<List<MailDTO>>(mailEntities);
            mails.ForEach(m => m.IsSent = false);

            return mails;
        }
        public async Task<List<MailDTO>> GetOutgoingMailsAsync(Guid userId, bool trackChanges)
        {
            var user = GetUserAndIfItExists(userId, trackChanges);

            var mailEntities = await _repository.Mail.GetOutgoingMailsForUserAsync(user, trackChanges);

            var mails = _mapper.Map<List<MailDTO>>(mailEntities);
            mails.ForEach(m => m.IsSent = true);

            return mails;
        }

        public async Task<List<MailDTO>> GetMailsForFolderAsync(Guid userId, Guid folderId, bool trackChanges)
        {
            var user = GetUserAndIfItExists(userId, trackChanges);

            var folder = (await _repository.Folder.GetFoldersAsync(user, trackChanges)).FirstOrDefault(f => f.FolderId == folderId);

            if (folder == null)
                throw new FolderNotFoundException(userId, folderId);

            var mails = await _repository.Mail.GetMailsInFolderForUserAsync(user, folder, trackChanges);

            var mailsDto = _mapper.Map<List<MailDTO>>(mails);
            mailsDto.ForEach(m => m.IsSent = user.Email == m.SenderMail);

            return mailsDto;
        }

        private User GetUserAndIfItExists(Guid userId, bool trackChanges)
        {
            var user = _repository.User.GetUserById(userId, false);
            if (user == null)
                throw new UserNotFoundException(userId);

            return user;
        }

        private User GetUserAndIfItExists(string email, bool trackChanges)
        {
            var user = _repository.User.GetUserByEmail(email, false);
            if (user == null)
                throw new UserNotFoundException(email);

            return user;
        }
    }
}
