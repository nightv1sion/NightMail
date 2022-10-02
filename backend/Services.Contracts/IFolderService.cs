using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IFolderService
    {
        Task CreateStandardFoldersForUserAsync(User user);
        Task<List<FolderDTO>> GetFoldersForUserAsync(Guid userId, bool trackChanges);
    }
}
