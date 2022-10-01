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

        public async Task<List<Folder>> GetFoldersForUserAsync(User user, bool trackChanges) => 
            await FindByCondition(f => f.User == user, trackChanges).ToListAsync();
    }
}
