using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IUserProfileImageRepository UserProfileImage { get; }
        IMailRepository Mail { get; }
        IFolderRepository Folder { get; } 
        IMailFolderRepository MailFolder { get; }
        Task SaveAsync();

    }
}
