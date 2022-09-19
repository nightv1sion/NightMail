using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MailRepository : RepositoryBase<Mail>, IMailRepository
    {
        public MailRepository(RepositoryContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Mail>> GetAllMailsForUserAsync(User user, bool trackChanges) =>
            await FindByCondition(e => e.Sender.Id == user.Id || e.Receiver.Id == user.Id, trackChanges)
                .ToListAsync();
        public void CreateMail(Mail mail) => Create(mail);

    }
}
