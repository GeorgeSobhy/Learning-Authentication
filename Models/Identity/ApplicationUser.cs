using Microsoft.AspNetCore.Identity;

namespace jwt.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
