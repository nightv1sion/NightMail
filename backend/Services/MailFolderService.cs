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
    public class MailFolderService : IMailFolderService
    {
        private readonly IRepositoryManager _repository;

        public MailFolderService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task CreateMailFolderAsync(Mail mail, Folder folder)
        {
            var mailFolder = new MailFolder() { MailId = mail.MailId, FolderId = folder.FolderId };

            _repository.MailFolder.CreateMailFolder(mailFolder);

            await _repository.SaveAsync();
        }
    }
}
