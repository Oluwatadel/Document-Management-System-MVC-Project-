using DMSMVC.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Pin { get; set; } = default!;
        public Staff? Staff { get; set; }
    }
}
