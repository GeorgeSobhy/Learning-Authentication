using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class RegisterDTO
    {
        [Required]
        public string FullName = null!;
        [Required]
        public string Username = null!;
        [Required]
        public string Password = null!;
    }
}
