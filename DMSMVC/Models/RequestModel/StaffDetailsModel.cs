using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.RequestModel
{
    public class StaffDetailsModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Enter the first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter the Last name")]
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public string? Email { get; set; }
        public string? StaffNumber { get; set; }
        public IFormFile ProfilePhotoUrl { get; set; } = default!;
        public string Level { get; set; }
        public Guid DepartmentId { get; set; }

    }
}
