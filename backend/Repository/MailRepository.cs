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

        public void CreateMail(Mail mail) => Create(mail);
        public async Task<List<Mail>> GetIncomingMailsForUserAsync(User user, bool trackChanges) => 
            await FindByCondition(m => m.Receiver.Id == user.Id, trackChanges)
            .Include(m => m.Receiver)
            .Include(m => m.Sender)
            .ToListAsync();

        public async Task<List<Mail>> GetOutgoingMailsForUserAsync(User user, bool trackChanges) =>
            await FindByCondition(m => m.Sender.Id == user.Id, trackChanges)
            .Include(m => m.Receiver)
            .Include(m => m.Sender)
            .ToListAsync();
    }
}
