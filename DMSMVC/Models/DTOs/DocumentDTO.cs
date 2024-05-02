
using DMSMVC.Models.Entities;

namespace DMSMVC.Models.DTOs
{
    public class DocumentDTO
    {
        //public string? DocumentId { get; set; }
        public Guid DocumentId { get; set; }
        public string? Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime TimeUploaded { get; set; } = DateTime.Now;
        public String DocumentUrl { get; set; } = default!;
        public string? Author { get; set; } = default!;
        public string? DepartmentName { get; set; } = default!;
    }

    

    
}
