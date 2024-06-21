using jwt.Models.DBContext;
using jwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace jwt.Seed
{
    public class Seeder
    {
        private readonly myContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public Seeder(IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager)
        {
            _context = serviceProvider.GetRequiredService<myContext>();
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = roleManager;
        }
        public async Task Intialize()
        {

            if(!_context.Users.Any()&& !_context.Roles.Any())
            {
                var user = new ApplicationUser()
                {
                    Email = "george@jwt.com",
                    FullName = "George Sobhy",
                    UserName = "George_Sohby"
                };
                await _userManager.CreateAsync(user, "QWEqwe123!@#");

                var role = new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                };
                await _roleManager.CreateAsync(role);

                await _userManager.AddToRoleAsync(user, role.NormalizedName);
            }

        }
    }
}
