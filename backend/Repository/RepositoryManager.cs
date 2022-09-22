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
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(new UserRepository(context));
            _userProfileImageRepository = new Lazy<IUserProfileImageRepository>(new UserProfileImageRepository(context));
        }
        public IUserRepository UserRepository => _userRepository.Value;
        public IUserProfileImageRepository UserProfileImageRepository => _userProfileImageRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
