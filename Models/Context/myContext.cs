using Microsoft.EntityFrameworkCore;
using jwt.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace jwt.Models.DBContext
{
    public class myContext: IdentityDbContext<ApplicationUser>
    {
        public myContext(DbContextOptions<myContext> options):base(options) { }
        
    }
}
