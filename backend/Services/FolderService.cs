using AutoMapper;
using Contracts;
using Entities.Models;
using Repository;
using Services.Contracts;
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
            Folder incomingFolder = new Folder() { Name = "IncomingMails", User = user, UserId = user.Id };
            Folder outgoingFolder = new Folder() { Name = "OutgoingMails", User = user, UserId = user.Id };
            Folder spamFolder = new Folder() { Name = "SpamMails", User = user, UserId = user.Id };

            _repository.Folder.CreateFolder(incomingFolder);
            _repository.Folder.CreateFolder(outgoingFolder);
            _repository.Folder.CreateFolder(spamFolder);

            await _repository.SaveAsync();
        }

    }
}
