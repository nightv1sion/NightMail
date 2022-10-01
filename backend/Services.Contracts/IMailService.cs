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
        Task CreateMailAsync(Guid senderId, MailDTO mailDto);
        Task<List<IncomingMailDTO>> GetIncomingMailsAsync(Guid userId, bool trackChanges);
        Task<List<OutgoingMailDTO>> GetOutgoingMailsAsync(Guid userId, bool trackChanges);
    }
}
