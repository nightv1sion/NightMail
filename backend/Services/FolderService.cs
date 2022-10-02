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

        public async Task CreateStandardFoldersForUserAsync(User user)
        {
            Folder incomingFolder = new Folder() { Name = "Incoming Emails", User = user, UserId = user.Id };
            Folder outgoingFolder = new Folder() { Name = "Outgoing Emails", User = user, UserId = user.Id };
            Folder spamFolder = new Folder() { Name = "Spam Emails", User = user, UserId = user.Id };

            _repository.Folder.CreateFolder(incomingFolder);
            _repository.Folder.CreateFolder(outgoingFolder);
            _repository.Folder.CreateFolder(spamFolder);

            await _repository.SaveAsync();
        }

        public async Task<List<FolderDTO>> GetFoldersForUserAsync(Guid userId, bool trackChanges)
        {
            var user = _repository.User.GetUserById(userId, trackChanges);
            if (user == null)
                throw new UserNotFoundException(userId);

            var folders = await _repository.Folder.GetFoldersForUserAsync(user, trackChanges);

            var foldersDto = _mapper.Map<List<FolderDTO>>(folders);
            return foldersDto;
        }
    }
}
