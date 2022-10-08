using AutoMapper;
using Contracts;
using Entities.ExceptionModels;
using Entities.Models;
using Repository;
using Services.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FolderService : IFolderService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly MailFolderService _mailFolderService;

        public FolderService(IRepositoryManager repository, IMapper mapper, MailFolderService mailFolderService)
        {
            _repository = repository;
            _mapper = mapper;
            _mailFolderService = mailFolderService;
        }

        public async Task IncludeMailAsync(Guid userId, Guid folderId, Guid mailId)
        {
            var user = GetUserAndCheckIfItExists(userId, false);
            var folder = await GetFolderAndCheckIfItExistsAsync(user, folderId, false);
            var mail = await _repository.Mail.GetMailForUserAsync(user, mailId, false);
            if (mail is null)
                throw new MailNotFoundException(mailId);

            await _mailFolderService.CreateMailFolderAsync(mail, folder);
        }
        public async Task<List<FolderDTO>> GetFoldersAsync(Guid userId, bool trackChanges)
        {
            var user = GetUserAndCheckIfItExists(userId, trackChanges);

            var folders = await _repository.Folder.GetFoldersAsync(user, trackChanges);

            var foldersDto = _mapper.Map<List<FolderDTO>>(folders);
            return foldersDto;
        }

        public async Task CreateFolderAsync(Guid userId, string folderName)
        {
            var user = GetUserAndCheckIfItExists(userId, false);

            var folder = new Folder() { Name = folderName, UserId = userId };

            _repository.Folder.CreateFolder(folder);
            await _repository.SaveAsync();
        }

        public async Task DeleteFolderAsync(Guid userId, Guid folderId)
        {
            var user = GetUserAndCheckIfItExists(userId, false);

            var folder = await GetFolderAndCheckIfItExistsAsync(user, folderId, false);

            _repository.Folder.DeleteFolder(folder);
            await _repository.SaveAsync();
        }

        public async Task UpdateFolderAsync(Guid userId, Guid folderId, string newName)
        {
            var user = GetUserAndCheckIfItExists(userId, false);
            var folder = await GetFolderAndCheckIfItExistsAsync(user, folderId, false);

            folder.Name = newName;

            _repository.Folder.UpdateFolder(folder);
            await _repository.SaveAsync();
        }

        private async Task<Folder> GetFolderAndCheckIfItExistsAsync(User user, Guid folderId, bool trackChanges)
        {
            var folder = await _repository.Folder.GetFolderAsync(user, folderId, trackChanges);
            if (folder is null)
                throw new FolderNotFoundException(user.Id, folderId);

            return folder;
        }

        private User GetUserAndCheckIfItExists(Guid userId, bool trackChanges)
        {
            var user = _repository.User.GetUserById(userId, trackChanges);
            if (user == null)
                throw new UserNotFoundException(userId);
            return user;
        }
    }
}
