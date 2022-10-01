using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFolderRepository
    {
        public void CreateFolder(Folder folder);
        Task<List<Folder>> GetFoldersForUserAsync(User user, bool trackChanges);

    }
}
