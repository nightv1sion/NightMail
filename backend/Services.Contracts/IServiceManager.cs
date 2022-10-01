using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        IUserService UserService { get; }
        IUserProfileImageService UserProfileImageService { get; }
        IFolderService FolderService { get;  }
        IMailFolderService MailFolderService { get; }
        IMailService MailService { get; }
    }
}
