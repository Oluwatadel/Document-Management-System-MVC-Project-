using System.ComponentModel.DataAnnotations;
namespace DMSMVC.Models.DTOs
{
    public class StaffDto
    { 
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; }
        public string? StaffNumber { get; set; }
        public string? Level { get; set; }
        public string? Role { get; set; }
        public string? phonenumber { get; set; }
        public string? DepartmentName { get; set; }
        public string? ImageUrl { get; set; }
    }

    //public class StaffRequestModel
    //{
    //    public string StaffNumber { get; set; }
    //    public string Level { get; set; }
    //    public string DepartmentName { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public GenderEnum Gender { get; set; }
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    [Compare("Password")]
    //    public string ConfirmPassword { get; set; }
    //    public string SecurityQuestion { get; set; }
    //    public string SecurityAnswer { get; set; }
    //}
}
