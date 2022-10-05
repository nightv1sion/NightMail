using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class FolderNotFoundException : NotFoundException
    {
        public FolderNotFoundException(Guid userId, Guid folderId) : base($"Folder with id: {folderId} for user with id: {userId} not found")
        {
        }
    }
}
