using DMSMVC.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.RequestModel
{
    public class DocumentRequestModel : Base
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public IFormFile File { get; set; } = default!;
        public string? Author { get; set; }
        public string? Password { get; set; }
        public bool IsDeleted { get; set; } = default;
    }
}
