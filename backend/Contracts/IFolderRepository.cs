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
        Task<List<Folder>> GetFoldersAsync(User user, bool trackChanges);
        Task<Folder> GetFolderAsync(User user, Guid folderId, bool trackChanges);
        void DeleteFolder(Folder folder);
        void UpdateFolder(Folder folder);
    }
}
