using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.RequestModel
{
    public class UserUpdateRequest
    {
        public string Email { get; set; }
        public string Pin { get; set; }
        public string ConfirmPin { get; set; }
    }
}
