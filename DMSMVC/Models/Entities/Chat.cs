using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Models.Entities
{
    public class Chat : Base
    {
        public string? SenderEmail { get; set; } = default!;
        public string? ReceiverEmail { get; set; } = default!;
        public string ChatReference { get; set; } = Guid.NewGuid().ToString().Substring(0, 6);
        public bool IsDeleted { get; set; } = false;
        public ICollection<ChatContent> ChatContents { get; set; } = new HashSet<ChatContent>();


    }
}
