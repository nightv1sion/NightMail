using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class FolderDTO
    {
        public Guid FolderId { get; set; }
        public string Name { get; set; }
        public int CountOfMails { get; set; }
    }
}
