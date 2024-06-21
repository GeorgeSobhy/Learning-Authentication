
using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class LoginDTO
    {

        public string? userName { get; set; }
        public string? password { get; set; }

    }
}
