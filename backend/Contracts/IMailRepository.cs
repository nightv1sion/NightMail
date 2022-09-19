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
        Task<IEnumerable<Mail>> GetAllMailsForUserAsync(User user, bool trackChanges);
        void CreateMail(Mail mail);
    }
}
