using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Models.Entities
{
    public class Document : Base
    {
        //public string DocumentId { get; set; } = Guid.NewGuid().ToString().Substring(0, 5);
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeUploaded { get; set; } = DateTime.Now;
        public string DocumentUrl { get; set; } = default!;
        public string? Author { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Department Department { get; set; }
        public Guid DepartmentId { get; set; }
        public Staff Staff { get; set; }
        public Guid StaffId {  get; set; }


    }
}
