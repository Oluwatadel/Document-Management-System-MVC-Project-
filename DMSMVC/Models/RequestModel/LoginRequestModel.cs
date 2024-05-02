using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.RequestModel
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; } = default!;
        [Required]
        [StringLength(6)]
        public String Pin { get; set; } = default!;
    }
}
