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
        Task<IEnumerable<MailDTO>> GetMailsAsync(string userEmail, bool trackChanges);
    }
}
