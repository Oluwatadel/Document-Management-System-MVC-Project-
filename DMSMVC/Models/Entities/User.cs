using System.ComponentModel.DataAnnotations;

namespace DMSMVC.Models.Entities
{
    public class User : Base
    {
        public string Email { get; set; }

        public string Pin { get; set; } = default!;
        public Staff? Staff { get; set; }

    }
}
