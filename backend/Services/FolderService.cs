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

        public FolderService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateStandardFoldersAsync(User user)
        {
            Folder incomingFolder = new Folder() { Name = "Incoming Emails", User = user, UserId = user.Id };
            Folder outgoingFolder = new Folder() { Name = "Outgoing Emails", User = user, UserId = user.Id };
            Folder spamFolder = new Folder() { Name = "Spam Emails", User = user, UserId = user.Id };

            _repository.Folder.CreateFolder(incomingFolder);
            _repository.Folder.CreateFolder(outgoingFolder);
            _repository.Folder.CreateFolder(spamFolder);

            await _repository.SaveAsync();
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
