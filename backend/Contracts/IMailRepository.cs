using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMailRepository
    {
        void CreateMail(Mail mail);
        Task<List<Mail>> GetIncomingMailsForUserAsync(User user, bool trackChanges);
        Task<List<Mail>> GetOutgoingMailsForUserAsync(User user, bool trackChanges);

    }
}
