using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private Lazy<IMailRepository> mailRepository;
        public RepositoryManager(RepositoryContext context)
        {
            mailRepository = new Lazy<IMailRepository>(new MailRepository(context));
            _context = context;
        }

        public IMailRepository Mail => mailRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
