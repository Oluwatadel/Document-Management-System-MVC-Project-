using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Models.Entities
{
    public class Department : Base
    {
        //public string DepartmentId { get; set; } = Guid.NewGuid().ToString().Substring(0,6);
        public string DepartmentName { get; set; } = default!;
        public string Acronym { get; set; } = default!;
        public string? HeadOfDepartmentStaffNumber { get; set; }
        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}
