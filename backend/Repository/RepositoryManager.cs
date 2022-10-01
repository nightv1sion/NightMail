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
        private Lazy<IUserRepository> _userRepository;
        private Lazy<IUserProfileImageRepository> _userProfileImageRepository;
        private Lazy<IMailRepository> _mailRepository;
        private Lazy<IFolderRepository> _folderRepository;
        private Lazy<IMailFolderRepository> _mailFolderRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(new UserRepository(context));
            _userProfileImageRepository = new Lazy<IUserProfileImageRepository>(new UserProfileImageRepository(context));
            _mailRepository = new Lazy<IMailRepository>(new MailRepository(context));
            _folderRepository = new Lazy<IFolderRepository>(new FolderRepository(context));
            _mailFolderRepository = new Lazy<IMailFolderRepository>(new MailFolderRepository(context));
        }
        public IUserRepository User => _userRepository.Value;
        public IUserProfileImageRepository UserProfileImage => _userProfileImageRepository.Value;
        public IMailRepository Mail => _mailRepository.Value;
        public IFolderRepository Folder => _folderRepository.Value;
        public IMailFolderRepository MailFolder => _mailFolderRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
