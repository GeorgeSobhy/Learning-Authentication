using jwt.Helpers;
using jwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace jwt.Services
{
    public class Auth : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        public Auth(UserManager<ApplicationUser> userManager, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
        }
        public async Task<string> login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
                return "not correct";

            if (!await _userManager.CheckPasswordAsync(user, password))
                return "not correct";

            //return jwt token

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("string", user.FullName));


            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtConfig.Key));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //JwtSecurityToken jwt = new JwtSecurityToken("iss", "aud", claims, null, DateTime.Now.AddDays(3), signingCredentials);
            JwtSecurityToken jwt = new JwtSecurityToken(_jwtConfig.Issuer, _jwtConfig.Audence, claims, null , DateTime.Now.AddDays(Convert.ToInt16(_jwtConfig.DurationInDays)), signingCredentials);


            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<bool> Register(string fullName, string username, string password)
        {

            ApplicationUser user = new ApplicationUser()
            {
                UserName = username,
                FullName = fullName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return false;

            return true;
        }
    }
}
