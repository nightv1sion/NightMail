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
    public class FolderRepository : RepositoryBase<Folder>, IFolderRepository
    {
        public FolderRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateFolder(Folder folder) => Create(folder);

        public async Task<List<Folder>> GetFoldersAsync(User user, bool trackChanges) => 
            await FindByCondition(f => f.User == user, trackChanges).ToListAsync();

        public async Task<Folder> GetFolderAsync(User user, Guid folderId, bool trackChanges) => 
            await FindByCondition(f => f.User.Id == user.Id, trackChanges).FirstOrDefaultAsync(f => f.FolderId == folderId);

        public void DeleteFolder(Folder folder) => Delete(folder);

        public void UpdateFolder(Folder folder) => Update(folder);
    }
}
