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

        public async Task CreateMailAsync(Guid senderId, MailDTO mailDto)
        {
            var sender = _repository.User.GetUserById(senderId, false);
            if (sender == null)
                throw new UserNotFoundException(senderId);

            var receiver = _repository.User.GetUserByEmail(mailDto.ReceiverMail, false);

            if (receiver == null)
                throw new UserNotFoundException(mailDto.ReceiverMail);

            var mail = _mapper.Map<Mail>(mailDto);

            mail.SenderId = sender.Id;
            mail.ReceiverId = receiver.Id;

            _repository.Mail.CreateMail(mail);
            await _repository.SaveAsync();

            receiver.Folders = await _repository.Folder.GetFoldersForUserAsync(receiver, false);
            var receiverIncomingFolder = receiver.Folders.FirstOrDefault(f => f.Name == "IncomingMails");

            await _mailFolderService.CreateMailFolderAsync(mail, receiverIncomingFolder);

            sender.Folders = await _repository.Folder.GetFoldersForUserAsync(sender, false);
            var senderOutgoingFolder = sender.Folders.FirstOrDefault(f => f.Name == "OutgoingMails");

            await _mailFolderService.CreateMailFolderAsync(mail, senderOutgoingFolder);
        }

        public async Task<List<IncomingMailDTO>> GetIncomingMailsAsync(Guid userId, bool trackChanges)
        {
            var user = _repository.User.GetUserById(userId, false);
            if (user == null)
                throw new UserNotFoundException(userId);

            var mailEntities = await _repository.Mail.GetIncomingMailsForUserAsync(user, trackChanges);

            var mails = _mapper.Map<List<IncomingMailDTO>>(mailEntities);

            return mails;
        }
        public async Task<List<OutgoingMailDTO>> GetOutgoingMailsAsync(Guid userId, bool trackChanges)
        {
            var user = _repository.User.GetUserById(userId, false);
            if (user == null)
                throw new UserNotFoundException(userId);

            var mailEntities = await _repository.Mail.GetOutgoingMailsForUserAsync(user, trackChanges);

            var mails = _mapper.Map<List<OutgoingMailDTO>>(mailEntities);

            return mails;
        }
    }
}
