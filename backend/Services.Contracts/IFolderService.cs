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
        Task CreateStandardFoldersAsync(User user);
        Task<List<FolderDTO>> GetFoldersAsync(Guid userId, bool trackChanges);
        Task CreateFolderAsync(Guid userId, string folderName);
        Task DeleteFolderAsync(Guid userId, Guid folderId);
        Task UpdateFolderAsync(Guid userId, Guid folderId, string newName);
    }
}
