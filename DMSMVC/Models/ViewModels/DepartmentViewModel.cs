using DMSMVC.Models.Entities;

namespace DMSMVC.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; } = default!;
        public string? Acronym { get; set; } = default!;
        public string? NameOfHod { get; set; }
        public ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
        public ICollection<Document> Documents { get; set; } = new HashSet<Document>();
    }

}
