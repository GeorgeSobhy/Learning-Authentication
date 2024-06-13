using jwt.Models.DBContext;
using jwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace jwt.Seed
{
    public class Seeder
    {
        private readonly DbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public Seeder(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<myContext>(); 
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }
        public async Task Intialize()
        {
            var user = new ApplicationUser()
            {
                Email = "george@jwt.com",
                FullName = "George Sobhy",
                UserName = "George_Sohby"
            };
            await _userManager.CreateAsync(user, "QWEqwe123!@#");
        }
    }
}
