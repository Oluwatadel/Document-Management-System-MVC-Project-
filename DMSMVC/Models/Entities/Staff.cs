using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Models.Entities
{
    public class Staff : Base
    {
         public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public GenderEnum? Gender { get; set; } = default!;
        public string? ProfilePhotoUrl { get; set; } 
        public string StaffNumber { get; set; } = default!;
        public string? Level { get; set; }
        public string Role { get; set; } = "Staff";
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }
        public Department Department { get; set; }
        public Guid DepartmentId { get; set; }
        public ICollection<Document?> Documents { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }



    }
}
