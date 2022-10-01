using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MailFolderRepository : RepositoryBase<MailFolder>, IMailFolderRepository
    {
        public MailFolderRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateMailFolder(MailFolder mailFolder) => Create(mailFolder);
    }
}
