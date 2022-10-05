using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IMailService
    {
        Task CreateMailAsync(Guid senderId, MailForCreateDTO mailDto);
        Task<List<MailDTO>> GetIncomingMailsAsync(Guid userId, bool trackChanges);
        Task<List<MailDTO>> GetOutgoingMailsAsync(Guid userId, bool trackChanges);
        Task<List<MailDTO>> GetMailsForFolderAsync(Guid userId, Guid folderId, bool trackChanges);

    }
}
